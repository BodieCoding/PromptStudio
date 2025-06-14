/**
 * Simple test to verify MCP client connection works
 */

const { spawn } = require('child_process');
const path = require('path');

// Test the MCP client connection
async function testMcpConnection() {
    console.log('🧪 Testing MCP Client Connection...');
    
    const serverPath = 'C:\\Code\\Promptlet\\dist\\PromptStudioMcpServer.exe';
    
    try {
        // Start the MCP server process
        console.log('Starting MCP server process...');
        const serverProcess = spawn(serverPath, [], {
            stdio: ['pipe', 'pipe', 'pipe']
        });

        let connected = false;
        let requestId = 1;

        // Handle server output
        serverProcess.stdout.on('data', (data) => {
            console.log('Server output:', data.toString());
            
            // Try to parse JSON responses
            const lines = data.toString().split('\n');
            for (const line of lines) {
                if (line.trim()) {
                    try {
                        const response = JSON.parse(line.trim());
                        console.log('📨 Received response:', JSON.stringify(response, null, 2));
                        
                        if (response.id === 1 && response.result) {
                            console.log('✅ Initialize successful!');
                            connected = true;
                            
                            // Now try to call a tool
                            setTimeout(() => {
                                console.log('📞 Calling prompt_templates_list tool...');
                                const toolRequest = {
                                    jsonrpc: '2.0',
                                    id: 2,
                                    method: 'tools/call',
                                    params: {
                                        name: 'prompt_templates_list',
                                        arguments: {}
                                    }
                                };
                                serverProcess.stdin.write(JSON.stringify(toolRequest) + '\n');
                            }, 1000);
                        }
                        
                        if (response.id === 2) {
                            console.log('✅ Tool call successful!');
                            console.log('Result:', JSON.stringify(response.result, null, 2));
                            serverProcess.kill();
                            console.log('🎉 MCP connection test completed successfully!');
                        }
                    } catch (error) {
                        // Not JSON, probably debug output
                    }
                }
            }
        });

        serverProcess.stderr.on('data', (data) => {
            console.log('Server stderr:', data.toString());
        });

        serverProcess.on('error', (error) => {
            console.error('❌ Server process error:', error);
        });

        serverProcess.on('exit', (code) => {
            console.log(`Server process exited with code: ${code}`);
        });

        // Send initialize request
        console.log('📤 Sending initialize request...');
        const initRequest = {
            jsonrpc: '2.0',
            id: 1,
            method: 'initialize',
            params: {
                protocolVersion: '2024-11-05',
                capabilities: {},
                clientInfo: {
                    name: 'test-client',
                    version: '1.0.0'
                }
            }
        };

        serverProcess.stdin.write(JSON.stringify(initRequest) + '\n');

        // Timeout after 10 seconds
        setTimeout(() => {
            if (!connected) {
                console.log('❌ Connection test timed out');
                serverProcess.kill();
            }
        }, 10000);

    } catch (error) {
        console.error('❌ Test failed:', error);
    }
}

// Run the test
testMcpConnection().catch(console.error);
