# 🔧 Guia de Instalação - Sistema de Gerenciamento de EPIs

## Pré-requisitos

### Obrigatórios
- **.NET 8.0 SDK** - [Download aqui](https://dotnet.microsoft.com/download/dotnet/8.0)
- **Git** - Para clonagem do repositório

### Verificação dos Pré-requisitos
```bash
# Verificar versão do .NET
dotnet --version
# Deve retornar 8.0.x ou superior

# Verificar Git
git --version
```

## 📥 Instalação Passo a Passo

### 1. Clonar o Repositório
```bash
git clone <url-do-repositorio>
cd EpiManagement
```

### 2. Restaurar Dependências
```bash
# Restaurar dependências de toda a solução
dotnet restore

# Ou restaurar individualmente
cd EpiManagement.Api
dotnet restore
cd ../EpiManagement.Web
dotnet restore
cd ..
```

### 3. Configurar Banco de Dados

#### Instalar Ferramenta EF (se necessário)
```bash
dotnet tool install --global dotnet-ef
```

#### Aplicar Migrações
```bash
cd EpiManagement.Api
dotnet ef database update
cd ..
```

### 4. Executar a Aplicação

#### Opção 1: Executar Manualmente (2 Terminais)

**Terminal 1 - API:**
```bash
cd EpiManagement.Api
dotnet run
```
A API estará disponível em: `http://localhost:5000`

**Terminal 2 - Frontend:**
```bash
cd EpiManagement.Web
dotnet run
```
O frontend estará disponível em: `http://localhost:5001`

#### Opção 2: Script de Execução (Linux/Mac)
```bash
# Criar script de execução
cat > run.sh << 'EOF'
#!/bin/bash
echo "Iniciando API..."
cd EpiManagement.Api
dotnet run &
API_PID=$!

echo "Aguardando API inicializar..."
sleep 5

echo "Iniciando Frontend..."
cd ../EpiManagement.Web
dotnet run &
WEB_PID=$!

echo "Aplicação iniciada!"
echo "API: http://localhost:5000"
echo "Frontend: http://localhost:5001"
echo "Pressione Ctrl+C para parar"

# Aguardar interrupção
trap "kill $API_PID $WEB_PID; exit" INT
wait
EOF

chmod +x run.sh
./run.sh
```

#### Opção 3: Script de Execução (Windows)
```batch
@echo off
echo Iniciando API...
start cmd /k "cd EpiManagement.Api && dotnet run"

echo Aguardando API inicializar...
timeout /t 5

echo Iniciando Frontend...
start cmd /k "cd EpiManagement.Web && dotnet run"

echo Aplicação iniciada!
echo API: http://localhost:5000
echo Frontend: http://localhost:5001
pause
```

## 🌐 Acessando a Aplicação

### URLs Principais
- **Frontend (Interface Web)**: http://localhost:5001
- **API REST**: http://localhost:5000
- **Swagger (Documentação da API)**: http://localhost:5000/swagger

### Funcionalidades Disponíveis
1. **Lista de EPIs** - Página inicial com todos os EPIs
2. **Dashboard** - Métricas e gráficos
3. **Criar EPI** - Formulário de criação
4. **Editar/Excluir** - Ações disponíveis na lista

## 🔧 Configurações Avançadas

### Alterar Portas (se necessário)

#### API (EpiManagement.Api/Program.cs)
```csharp
app.Run("http://0.0.0.0:NOVA_PORTA");
```

#### Frontend (EpiManagement.Web/Program.cs)
```csharp
app.Run("http://0.0.0.0:NOVA_PORTA");
```

#### Atualizar URL da API no Frontend
```csharp
// EpiManagement.Web/Program.cs
builder.Services.AddHttpClient<EpiApiService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:NOVA_PORTA_API/");
});
```

### Configurar Banco de Dados Diferente

#### SQL Server
```json
// EpiManagement.Api/appsettings.json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=EpiManagement;Trusted_Connection=true;TrustServerCertificate=true;"
  }
}
```

```csharp
// EpiManagement.Api/Program.cs
builder.Services.AddDbContext<EpiDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
```

#### MySQL
```json
// EpiManagement.Api/appsettings.json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=EpiManagement;Uid=root;Pwd=password;"
  }
}
```

## 🐛 Solução de Problemas

### Erro: "dotnet command not found"
**Solução:** Instalar .NET 8.0 SDK e reiniciar o terminal

### Erro: "dotnet-ef command not found"
**Solução:**
```bash
dotnet tool install --global dotnet-ef
export PATH="$PATH:$HOME/.dotnet/tools"
```

### Erro: "Port already in use"
**Solução:** Alterar as portas no Program.cs ou parar processos que estão usando as portas:
```bash
# Linux/Mac
lsof -ti:5000 | xargs kill -9
lsof -ti:5001 | xargs kill -9

# Windows
netstat -ano | findstr :5000
taskkill /PID <PID> /F
```

### Erro: "Database connection failed"
**Solução:** Verificar se o banco foi criado corretamente:
```bash
cd EpiManagement.Api
dotnet ef database drop
dotnet ef database update
```

### Erro: "CORS policy"
**Solução:** Verificar se o CORS está configurado corretamente na API (já configurado no projeto)

### Frontend não consegue acessar a API
**Solução:** Verificar se:
1. A API está rodando na porta correta (5000)
2. A URL da API está correta no frontend
3. Não há firewall bloqueando as conexões

## 📊 Dados de Teste

O sistema já inclui dados de exemplo. Para resetar os dados:
```bash
cd EpiManagement.Api
dotnet ef database drop
dotnet ef database update
```

## 🚀 Deploy em Produção

### IIS (Windows)
1. Publicar a aplicação: `dotnet publish -c Release`
2. Configurar IIS com .NET 8.0 Hosting Bundle
3. Criar sites para API e Frontend
4. Configurar string de conexão de produção

### Linux (Nginx + Systemd)
1. Publicar: `dotnet publish -c Release`
2. Configurar Nginx como proxy reverso
3. Criar serviços systemd para API e Frontend
4. Configurar SSL com Let's Encrypt

### Docker
```dockerfile
# Dockerfile para API
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY . .
EXPOSE 5000
ENTRYPOINT ["dotnet", "EpiManagement.Api.dll"]
```

## 📞 Suporte

Em caso de problemas:
1. Verificar se todos os pré-requisitos estão instalados
2. Consultar a seção de solução de problemas
3. Verificar os logs da aplicação
4. Entrar em contato com o desenvolvedor

