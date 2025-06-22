# PromptStudio Docker Stack Troubleshooting Guide

This guide helps you diagnose and resolve common issues with the PromptStudio Docker stack.

## Quick Start

```powershell
# Start the stack
.\start-stack.ps1

# Start in development mode with logs
.\start-stack.ps1 -Mode development -Logs

# Clean rebuild
.\start-stack.ps1 -Clean

# Stop all services
.\start-stack.ps1 -Stop
```

## Architecture Overview

The PromptStudio stack consists of three main services:

1. **SQL Server** (`sqlserver`) - Database service on port 1433
2. **PromptStudio** (`promptstudio`) - Web application on port 5000
3. **MCP Server** (`mcp-server`) - Model Context Protocol server on port 3001

### Service Dependencies

```
SQL Server → PromptStudio → MCP Server
```

Each service waits for the previous one to be healthy before starting.

## Common Issues and Solutions

### 1. Docker Desktop Not Running

**Symptoms:**
- Error: "Cannot connect to the Docker daemon"
- Services fail to start

**Solution:**
```powershell
# Start Docker Desktop and wait for it to fully load
# Then retry the stack startup
.\start-stack.ps1
```

### 2. Port Conflicts

**Symptoms:**
- Error: "Port already in use"
- Services start but are not accessible

**Check ports:**
```powershell
# Check if ports are in use
netstat -an | findstr "1433\|5000\|3001"

# Kill processes using these ports (if safe to do so)
# Or stop conflicting Docker containers
docker ps
docker stop <container-name>
```

**Alternative ports (modify docker-compose.yml):**
```yaml
# Change host ports if needed
ports:
  - "1434:1433"  # SQL Server
  - "5001:80"    # PromptStudio
  - "3002:3001"  # MCP Server
```

### 3. SQL Server Connection Issues

**Symptoms:**
- PromptStudio fails to start
- Database connection errors in logs

**Check SQL Server:**
```powershell
# Check SQL Server logs
docker-compose logs sqlserver

# Test connection manually
docker exec -it promptlet-sqlserver-1 /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P "Two3RobotDuckTag!" -Q "SELECT 1"
```

**Common fixes:**
```powershell
# Restart SQL Server
docker-compose restart sqlserver

# Clean restart with volume reset
docker-compose down -v
.\start-stack.ps1 -Clean
```

### 4. PromptStudio Health Check Failures

**Symptoms:**
- PromptStudio container runs but health check fails
- MCP Server cannot connect

**Debug steps:**
```powershell
# Check PromptStudio logs
docker-compose logs promptstudio

# Test health endpoint manually
curl http://localhost:5000/health

# Check if application is running inside container
docker exec -it promptlet-promptstudio-1 curl -f http://localhost:80/health
```

**Common fixes:**
- Ensure the `/health` endpoint is properly configured in Program.cs
- Check that ASP.NET Core is binding to all interfaces (`http://+:80`)
- Verify database connection string in appsettings.json

### 5. MCP Server Connection Issues

**Symptoms:**
- MCP Server health check fails
- Cannot connect from Claude/other MCP clients

**Important Note:**
The MCP Server uses stdio transport by default, not HTTP. Health check failures are normal for stdio-based servers.

**Debug MCP Server:**
```powershell
# Check MCP Server logs
docker-compose logs mcp-server

# Test if it's running
docker exec -it promptlet-mcp-server-1 ps aux

# Test PromptStudio API connection from MCP server
docker exec -it promptlet-mcp-server-1 curl http://promptstudio:80/health
```

### 6. Development Mode Issues

**Symptoms:**
- Hot reload not working
- Changes not reflected

**Solutions:**
```powershell
# Ensure using development override
.\start-stack.ps1 -Mode development

# Check volume mounts are correct
docker-compose -f docker-compose.yml -f docker-compose.dev.yml config

# Restart in development mode
.\start-stack.ps1 -Stop
.\start-stack.ps1 -Mode development -Clean
```

### 7. Build Failures

**Symptoms:**
- Image build errors
- Missing dependencies

**Solutions:**
```powershell
# Clean rebuild all images
.\start-stack.ps1 -Clean

# Build individual services
docker-compose build sqlserver
docker-compose build promptstudio
docker-compose build mcp-server

# Check Docker build context
docker build -f PromptStudio/Dockerfile .
docker build -f mcp-server/Dockerfile ./mcp-server
```

## Diagnostic Commands

### Service Status
```powershell
# Quick status overview
.\start-stack.ps1 -Status  # (if you add this feature)

# Manual status check
docker-compose ps
docker-compose ps --format "table {{.Service}}\t{{.State}}\t{{.Health}}"
```

### Logs
```powershell
# All services
docker-compose logs

# Specific service
docker-compose logs sqlserver
docker-compose logs promptstudio
docker-compose logs mcp-server

# Follow logs in real-time
docker-compose logs -f
```

### Network Connectivity
```powershell
# Test internal network connectivity
docker exec -it promptlet-promptstudio-1 ping sqlserver
docker exec -it promptlet-mcp-server-1 ping promptstudio

# Test external access
curl http://localhost:5000/health
curl http://localhost:3001  # May not respond for stdio MCP servers
```

### Resource Usage
```powershell
# Container resource usage
docker stats

# System resource usage
docker system df
```

## Environment Variables

### Production Environment
```bash
# SQL Server
SA_PASSWORD=Two3RobotDuckTag!
ACCEPT_EULA=Y

# PromptStudio
ConnectionStrings__DefaultConnection=Server=sqlserver,1433;Database=PromptStudio;User Id=sa;Password=Two3RobotDuckTag!;TrustServerCertificate=true;
MCP_SERVER_URL=http://mcp-server:3001

# MCP Server
NODE_ENV=production
PROMPTSTUDIO_API_URL=http://promptstudio:80
```

### Development Environment
```bash
# Additional PromptStudio settings
ASPNETCORE_ENVIRONMENT=Development
DOTNET_USE_POLLING_FILE_WATCHER=true
DOTNET_WATCH_RESTART_ON_RUDE_EDIT=true

# Additional MCP Server settings
NODE_ENV=development
DEBUG=mcp:*
```

## Performance Optimization

### Resource Limits
Add to docker-compose.yml:
```yaml
services:
  sqlserver:
    deploy:
      resources:
        limits:
          memory: 2G
          cpus: '1.0'
  
  promptstudio:
    deploy:
      resources:
        limits:
          memory: 1G
          cpus: '0.5'
```

### Volume Optimization
```yaml
# Use bind mounts for development only
# Use named volumes for production data persistence
volumes:
  sqlserver-data:
    driver: local
```

## Security Considerations

### Production Deployment
1. Change default SQL Server password
2. Use environment file for secrets
3. Configure proper network policies
4. Enable HTTPS/TLS termination

### Environment File Example
Create `.env` file:
```bash
SA_PASSWORD=YourSecurePassword123!
PROMPTSTUDIO_SECRET_KEY=your-secret-key-here
```

## Getting Help

### Check Stack Status
```powershell
# Service health overview
docker-compose ps --format "table {{.Service}}\t{{.State}}\t{{.Health}}\t{{.Ports}}"

# Service logs
docker-compose logs --tail=50 --timestamps
```

### Reset Everything
```powershell
# Nuclear option - reset everything
docker-compose down -v --remove-orphans
docker system prune -f
.\start-stack.ps1 -Clean
```

### Submit Bug Reports
When reporting issues, include:

1. Operating system and Docker version
2. Output of `docker-compose ps`
3. Relevant logs from `docker-compose logs [service]`
4. Steps to reproduce the issue
5. Expected vs actual behavior

### Useful Resources
- [Docker Compose Documentation](https://docs.docker.com/compose/)
- [ASP.NET Core Health Checks](https://docs.microsoft.com/en-us/aspnet/core/host-and-deploy/health-checks)
- [Model Context Protocol](https://modelcontextprotocol.io/)
