#!/bin/bash

# Define variables
SOLUTION_NAME="PopVault"
SRC_DIR="src"

# Colors for output
GREEN='\033[0;32m'
NC='\033[0m' # No Color

echo -e "${GREEN}Starting Solution Setup for $SOLUTION_NAME...${NC}"

# Create solution file
echo -e "${GREEN}Creating Solution file...${NC}"
dotnet new sln -n $SOLUTION_NAME

# Create src directory
mkdir -p $SRC_DIR

# Create Projects
echo -e "${GREEN}Creating Projects...${NC}"

# 1. Domain
dotnet new classlib -n "$SOLUTION_NAME.Domain" -o "$SRC_DIR/$SOLUTION_NAME.Domain"
dotnet sln add "$SRC_DIR/$SOLUTION_NAME.Domain/$SOLUTION_NAME.Domain.csproj"

# 2. Application
dotnet new classlib -n "$SOLUTION_NAME.Application" -o "$SRC_DIR/$SOLUTION_NAME.Application"
dotnet sln add "$SRC_DIR/$SOLUTION_NAME.Application/$SOLUTION_NAME.Application.csproj"

# 3. Infrastructure
dotnet new classlib -n "$SOLUTION_NAME.Infrastructure" -o "$SRC_DIR/$SOLUTION_NAME.Infrastructure"
dotnet sln add "$SRC_DIR/$SOLUTION_NAME.Infrastructure/$SOLUTION_NAME.Infrastructure.csproj"

# 4. API
dotnet new webapi -n "$SOLUTION_NAME.API" -o "$SRC_DIR/$SOLUTION_NAME.API"
dotnet sln add "$SRC_DIR/$SOLUTION_NAME.API/$SOLUTION_NAME.API.csproj"

# Add References
echo -e "${GREEN}Adding Project References...${NC}"

# Application -> Domain
dotnet add "$SRC_DIR/$SOLUTION_NAME.Application/$SOLUTION_NAME.Application.csproj" reference "$SRC_DIR/$SOLUTION_NAME.Domain/$SOLUTION_NAME.Domain.csproj"

# Infrastructure -> Application
dotnet add "$SRC_DIR/$SOLUTION_NAME.Infrastructure/$SOLUTION_NAME.Infrastructure.csproj" reference "$SRC_DIR/$SOLUTION_NAME.Application/$SOLUTION_NAME.Application.csproj"

# Infrastructure -> Domain
dotnet add "$SRC_DIR/$SOLUTION_NAME.Infrastructure/$SOLUTION_NAME.Infrastructure.csproj" reference "$SRC_DIR/$SOLUTION_NAME.Domain/$SOLUTION_NAME.Domain.csproj"

# API -> Application
dotnet add "$SRC_DIR/$SOLUTION_NAME.API/$SOLUTION_NAME.API.csproj" reference "$SRC_DIR/$SOLUTION_NAME.Application/$SOLUTION_NAME.Application.csproj"

# API -> Infrastructure
dotnet add "$SRC_DIR/$SOLUTION_NAME.API/$SOLUTION_NAME.API.csproj" reference "$SRC_DIR/$SOLUTION_NAME.Infrastructure/$SOLUTION_NAME.Infrastructure.csproj"

echo -e "${GREEN}Solution Setup Complete! Building to verify references...${NC}"

dotnet build $SOLUTION_NAME.sln

echo -e "${GREEN}Done.${NC}"
