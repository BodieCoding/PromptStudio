# PowerShell script to test form submission with anti-forgery token
$baseUrl = "http://localhost:5131"
$session = New-Object Microsoft.PowerShell.Commands.WebRequestSession

# First, get the form page to extract the anti-forgery token
Write-Host "Getting form page..."
$getResponse = Invoke-WebRequest -Uri "$baseUrl/Prompts/Create" -WebSession $session
Write-Host "GET Status: $($getResponse.StatusCode)"

# Extract anti-forgery token using regex since ParsedHtml might not work
$tokenPattern = 'name="__RequestVerificationToken"[^>]*value="([^"]*)"'
if ($getResponse.Content -match $tokenPattern) {
    $token = $matches[1]
    Write-Host "Anti-forgery token: $token"
} else {
    Write-Host "Could not find anti-forgery token"
    exit 1
}

# Prepare form data as URL-encoded string
$formFields = @(
    "__RequestVerificationToken=$([System.Web.HttpUtility]::UrlEncode($token))"
    "CollectionId=1"
    "PromptTemplate.Name=Test%20Prompt%20Debug"
    "PromptTemplate.Description=Testing%20validation%20issues"
    "PromptTemplate.Content=This%20is%20a%20test%20prompt%20with%20%7B%7Bvariable1%7D%7D%20and%20%7B%7Bvariable2%7D%7D"
)
$formData = $formFields -join "&"

Write-Host "Form data: $formData"

# Submit the form
Write-Host "Submitting form..."
try {
    $postResponse = Invoke-WebRequest -Uri "$baseUrl/Prompts/Create" -Method POST -Body $formData -ContentType "application/x-www-form-urlencoded" -WebSession $session -MaximumRedirection 0 -ErrorAction SilentlyContinue
    Write-Host "POST Status: $($postResponse.StatusCode)"
    
    if ($postResponse.StatusCode -eq 200) {
        Write-Host "Form submission returned 200 - likely validation errors or page redisplay"
    } elseif ($postResponse.StatusCode -eq 302) {
        Write-Host "Form submission successful - redirected to: $($postResponse.Headers.Location)"
    }
    Write-Host "Response Headers:"
    $postResponse.Headers.GetEnumerator() | ForEach-Object { Write-Host "  $($_.Key): $($_.Value)" }
} catch {
    Write-Host "Error during form submission: $($_.Exception.Message)"
}
