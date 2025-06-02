#!/usr/bin/env node

// Quick test script to verify the refactored MCP server client works with PromptApiController
const axios = require('axios');

const PROMPTSTUDIO_BASE_URL = 'http://localhost:5131';

class PromptStudioClient {
  constructor(baseURL) {
    this.baseURL = baseURL;
  }

  async getCollections() {
    const response = await axios.get(`${this.baseURL}/api/prompts/collections`);
    return response.data;
  }

  async getPromptTemplate(id) {
    const response = await axios.get(`${this.baseURL}/api/prompts/prompts/${id}`);
    return response.data;
  }

  async generateCsvTemplate(templateId) {
    const response = await axios.get(`${this.baseURL}/api/prompts/prompt-templates/${templateId}/csv-template`, {
      responseType: 'text'
    });
    return response.data;
  }
}

async function testMcpClient() {
  console.log('🧪 Testing refactored MCP client with PromptApiController...\n');
  
  const client = new PromptStudioClient(PROMPTSTUDIO_BASE_URL);
  
  try {
    // Test 1: Get collections
    console.log('1. Testing getCollections()...');
    const collections = await client.getCollections();
    console.log(`✓ Found ${collections.length} collections`);
    console.log(`   Collections: ${collections.map(c => c.name).join(', ')}\n`);
    
    // Test 2: Get a prompt template
    console.log('2. Testing getPromptTemplate()...');
    const template = await client.getPromptTemplate(1);
    console.log(`✓ Retrieved template: "${template.name}"`);
    console.log(`   Variables: ${template.variableCount} variables\n`);
    
    // Test 3: Generate CSV template
    console.log('3. Testing generateCsvTemplate()...');
    const csvContent = await client.generateCsvTemplate(1);
    console.log(`✓ Generated CSV template (${csvContent.length} characters)`);
    console.log(`   Preview: ${csvContent.substring(0, 100)}...\n`);
    
    console.log('🎉 All tests passed! The refactored MCP server should work correctly.');
    
  } catch (error) {
    console.error('❌ Test failed:', error.message);
    if (error.response) {
      console.error(`   Status: ${error.response.status}`);
      console.error(`   Data: ${JSON.stringify(error.response.data)}`);
    }
  }
}

testMcpClient();
