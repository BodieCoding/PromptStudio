const axios = require('axios');

console.log('Testing PromptApiController endpoints...');

async function test() {
  try {
    console.log('1. Testing collections endpoint...');
    const response = await axios.get('http://localhost:5131/api/prompts/collections');
    console.log(`✓ Collections: ${response.data.length} found`);
    
    console.log('2. Testing prompts endpoint...');
    const prompts = await axios.get('http://localhost:5131/api/prompts/prompts');
    console.log(`✓ Prompts: ${prompts.data.length} found`);
    
    if (prompts.data.length > 0) {
      const firstPromptId = prompts.data[0].id;
      console.log('3. Testing CSV template endpoint...');
      const csv = await axios.get(`http://localhost:5131/api/prompts/prompt-templates/${firstPromptId}/csv-template`, {
        responseType: 'text'
      });
      console.log(`✓ CSV template generated (${csv.data.length} chars)`);
    }
    
    console.log('\n🎉 All refactored endpoints are working!');
  } catch (error) {
    console.error('❌ Error:', error.message);
  }
}

test();
