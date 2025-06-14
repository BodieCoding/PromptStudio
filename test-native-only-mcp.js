#!/usr/bin/env node
/**
 * Test script for the native-only PromptStudio MCP extension
 * This tests the VS Code extension communicating directly with the standalone MCP server
 */

const { spawn } = require('child_process');
const path = require('path');

const MCP_SERVER_PATH = path.join(__dirname, 'dist', 'PromptStudioMcpServer.exe');

class McpTestClient {
    constructor(serverPath) {
        this.serverPath = serverPath;
        this.requestId = 1;
        this.pendingRequests = new Map();
        this.buffer = '';
    }

    async connect() {
        return new Promise((resolve, reject) => {
            console.log(`🚀 Starting MCP server: ${this.serverPath}`);
            
            this.serverProcess = spawn(this.serverPath, ['stdio'], {
                stdio: ['pipe', 'pipe', 'pipe']
            });

            this.serverProcess.stdout.on('data', (data) => {
                this.buffer += data.toString();
                this.processBuffer();
            });

            this.serverProcess.stderr.on('data', (data) => {
                console.error('❌ MCP Server Error:', data.toString());
            });

            this.serverProcess.on('error', (error) => {
                console.error('❌ Failed to start MCP server:', error);
                reject(error);
            });

            // Initialize MCP connection
            this.sendRequest('initialize', {
                protocolVersion: '2024-11-05',
                capabilities: {},
                clientInfo: { name: 'PromptStudio-Test', version: '1.0.0' }
            }).then(() => {
                console.log('✅ MCP server connected successfully');
                resolve();
            }).catch(reject);
        });
    }

    processBuffer() {
        const lines = this.buffer.split('\\n');
        this.buffer = lines.pop() || '';

        for (const line of lines) {
            if (line.trim()) {
                try {
                    const response = JSON.parse(line);
                    this.handleResponse(response);
                } catch (error) {
                    console.error('❌ Failed to parse MCP response:', error, line);
                }
            }
        }
    }

    handleResponse(response) {
        const pending = this.pendingRequests.get(response.id);
        if (pending) {
            this.pendingRequests.delete(response.id);
            if (response.error) {
                pending.reject(new Error(response.error.message || 'MCP Error'));
            } else {
                pending.resolve(response.result);
            }
        }
    }

    async sendRequest(method, params) {
        return new Promise((resolve, reject) => {
            const id = this.requestId++;
            const request = {
                jsonrpc: '2.0',
                method,
                params,
                id
            };

            this.pendingRequests.set(id, { resolve, reject });

            const message = JSON.stringify(request) + '\\n';
            this.serverProcess.stdin.write(message);

            // Timeout after 10 seconds
            setTimeout(() => {
                if (this.pendingRequests.has(id)) {
                    this.pendingRequests.delete(id);
                    reject(new Error('Request timeout'));
                }
            }, 10000);
        });
    }

    async testListCollections() {
        console.log('🧪 Testing ListCollections...');
        try {
            const result = await this.sendRequest('tools/call', {
                name: 'ListCollections',
                arguments: {}
            });
            console.log('✅ ListCollections result:', JSON.stringify(result, null, 2));
            return true;
        } catch (error) {
            console.error('❌ ListCollections failed:', error);
            return false;
        }
    }

    async testListPrompts() {
        console.log('🧪 Testing ListPrompts...');
        try {
            const result = await this.sendRequest('tools/call', {
                name: 'ListPrompts',
                arguments: {}
            });
            console.log('✅ ListPrompts result:', JSON.stringify(result, null, 2));
            return true;
        } catch (error) {
            console.error('❌ ListPrompts failed:', error);
            return false;
        }
    }

    async disconnect() {
        if (this.serverProcess) {
            this.serverProcess.kill();
        }
    }
}

async function runTests() {
    console.log('🔬 PromptStudio Native-Only MCP Extension Test');
    console.log('================================================');
    console.log('');

    const client = new McpTestClient(MCP_SERVER_PATH);
    
    try {
        await client.connect();
        
        const tests = [
            () => client.testListCollections(),
            () => client.testListPrompts()
        ];

        let passed = 0;
        let failed = 0;

        for (const test of tests) {
            const result = await test();
            if (result) {
                passed++;
            } else {
                failed++;
            }
        }

        console.log('');
        console.log('📊 Test Results:');
        console.log(`   ✅ Passed: ${passed}`);
        console.log(`   ❌ Failed: ${failed}`);
        console.log(`   📈 Success Rate: ${Math.round((passed / (passed + failed)) * 100)}%`);

        if (failed === 0) {
            console.log('');
            console.log('🎉 All tests passed! The native-only MCP extension is working correctly.');
        } else {
            console.log('');
            console.log('⚠️  Some tests failed. Please check the MCP server implementation.');
        }

    } catch (error) {
        console.error('❌ Test suite failed:', error);
        process.exit(1);
    } finally {
        await client.disconnect();
    }
}

if (require.main === module) {
    runTests().catch(console.error);
}
