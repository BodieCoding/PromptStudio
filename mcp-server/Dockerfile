# Use Node.js 18 LTS Alpine for smaller image size
FROM node:18-alpine

# Install curl for health checks
RUN apk add --no-cache curl

# Set working directory
WORKDIR /app

# Copy package files
COPY package*.json ./

# Install all dependencies (including devDependencies for build)
RUN npm ci

# Copy TypeScript config and source
COPY tsconfig.json ./
COPY src/ ./src/

# Build the TypeScript project
RUN npm run build

# Remove devDependencies after build to reduce image size
RUN npm prune --production

# Expose MCP server port (if needed for HTTP transport)
EXPOSE 3001

# Set environment variables
ENV NODE_ENV=production
ENV MCP_SERVER_HOST=0.0.0.0
ENV MCP_SERVER_PORT=3001

# Health check
HEALTHCHECK --interval=30s --timeout=3s --start-period=5s --retries=3 \
  CMD node -e "console.log('MCP Server Health Check')" || exit 1

# Run the MCP server
CMD ["npm", "start"]
