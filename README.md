# Sistema de Gerenciamento de EPIs

Uma aplicação full-stack desenvolvida em .NET 8.0 para gerenciamento de Equipamentos de Proteção Individual (EPIs), incluindo CRUD completo, dashboard com métricas e funcionalidades de filtro.

## 🚀 Tecnologias Utilizadas

### Backend
- **.NET 8.0** - Framework principal
- **ASP.NET Core Web API** - API REST
- **Entity Framework Core** - ORM para acesso a dados
- **SQLite** - Banco de dados para desenvolvimento
- **C#** - Linguagem de programação

### Frontend
- **ASP.NET Core MVC** - Framework web
- **Bootstrap 5** - Framework CSS
- **Chart.js** - Biblioteca para gráficos
- **Font Awesome** - Ícones
- **HTML5/CSS3/JavaScript** - Tecnologias web

## 📋 Funcionalidades

### ✅ CRUD de EPIs (Obrigatório)
- **Listagem** de todos os EPIs cadastrados
- **Criação** de novos EPIs
- **Visualização** de detalhes de um EPI
- **Edição** de EPIs existentes
- **Exclusão** de EPIs
- **Filtros** por nome e CA
- **Validações** completas nos formulários

### ✅ Dashboard com Métricas (Diferencial)
- **Total de EPIs** cadastrados
- **EPIs válidos, vencidos e próximos do vencimento**
- **Distribuição por categoria** (gráfico de pizza)
- **Lista de EPIs vencendo em 30 dias**
- **Indicadores visuais** de status

### 📊 Campos do EPI
- **Id** - Identificador único (auto-incremento)
- **Nome** - Nome do EPI (obrigatório, máx. 200 caracteres)
- **CA** - Certificado de Aprovação (obrigatório, único, > 0)
- **Descrição** - Descrição opcional (máx. 500 caracteres)
- **Validade** - Data de validade (obrigatório)
- **Categoria** - Categoria do EPI (obrigatório, máx. 100 caracteres)

## 🏗️ Arquitetura

```
EpiManagement/
├── EpiManagement.Api/          # API REST
│   ├── Controllers/            # Controllers da API
│   ├── Models/                 # Modelos de dados
│   ├── DTOs/                   # Data Transfer Objects
│   ├── Data/                   # DbContext e configurações
│   └── Migrations/             # Migrações do banco
├── EpiManagement.Web/          # Frontend MVC
│   ├── Controllers/            # Controllers MVC
│   ├── Views/                  # Views Razor
│   ├── Models/                 # ViewModels
│   └── Services/               # Serviços para consumir API
└── EpiManagement.sln           # Solução do projeto
```

## 🔧 Instalação e Configuração

### Pré-requisitos
- **.NET 8.0 SDK** ou superior
- **Git** para clonagem do repositório

### Passos para Instalação

1. **Clone o repositório**
```bash
git clone <url-do-repositorio>
cd EpiManagement
```

2. **Restaure as dependências**
```bash
dotnet restore
```

3. **Configure o banco de dados**
```bash
cd EpiManagement.Api
dotnet ef database update
```

4. **Execute a API (Terminal 1)**
```bash
cd EpiManagement.Api
dotnet run
```
A API estará disponível em: `http://localhost:5000`

5. **Execute o Frontend (Terminal 2)**
```bash
cd EpiManagement.Web
dotnet run
```
O frontend estará disponível em: `http://localhost:5001`

## 📚 Documentação da API

### Base URL
```
http://localhost:5000/api
```

### Endpoints Disponíveis

#### 📋 Listar EPIs
```http
GET /epis
```
**Parâmetros de Query (opcionais):**
- `nome` - Filtrar por nome (string)
- `ca` - Filtrar por CA (integer)

**Resposta:**
```json
[
  {
    "id": 1,
    "nome": "Capacete de Segurança",
    "ca": 12345,
    "descricao": "Capacete de segurança para proteção da cabeça",
    "validade": "2026-03-10T00:00:00",
    "categoria": "Proteção da Cabeça",
    "estaVencido": false,
    "proximoDoVencimento": false
  }
]
```

#### 🔍 Obter EPI por ID
```http
GET /epis/{id}
```

#### ➕ Criar EPI
```http
POST /epis
Content-Type: application/json

{
  "nome": "Novo EPI",
  "ca": 99999,
  "descricao": "Descrição opcional",
  "validade": "2025-12-31",
  "categoria": "Proteção da Cabeça"
}
```

#### ✏️ Atualizar EPI
```http
PUT /epis/{id}
Content-Type: application/json

{
  "nome": "EPI Atualizado",
  "ca": 99999,
  "descricao": "Nova descrição",
  "validade": "2025-12-31",
  "categoria": "Proteção da Cabeça"
}
```

#### 🗑️ Excluir EPI
```http
DELETE /epis/{id}
```

#### 📊 Métricas do Dashboard
```http
GET /epis/dashboard
```

**Resposta:**
```json
{
  "totalEpis": 4,
  "episVencidos": 1,
  "episProximosDoVencimento": 1,
  "episPorCategoria": {
    "Proteção da Cabeça": 1,
    "Proteção dos Olhos": 1,
    "Proteção das Mãos": 1,
    "Proteção dos Pés": 1
  },
  "episVencendoEm30Dias": [...]
}
```

## 🎯 Validações Implementadas

### Backend (API)
- **Nome**: Obrigatório, máximo 200 caracteres
- **CA**: Obrigatório, único, maior que 0
- **Validade**: Obrigatório, formato de data válido
- **Categoria**: Obrigatório, máximo 100 caracteres
- **Descrição**: Opcional, máximo 500 caracteres

### Frontend (MVC)
- **Validação client-side** com JavaScript
- **Validação server-side** com Data Annotations
- **Feedback visual** para sucesso e erro
- **Mensagens de erro** personalizadas

## 🎨 Interface do Usuário

### Características
- **Design responsivo** com Bootstrap 5
- **Indicadores visuais** de status (válido, vencido, próximo do vencimento)
- **Filtros intuitivos** por nome e CA
- **Dashboard interativo** com gráficos
- **Navegação simples** e intuitiva
- **Feedback visual** para todas as operações

### Páginas Disponíveis
- **Lista de EPIs** - Visualização e filtros
- **Criar EPI** - Formulário de criação
- **Editar EPI** - Formulário de edição
- **Detalhes do EPI** - Visualização completa
- **Confirmar Exclusão** - Confirmação de exclusão
- **Dashboard** - Métricas e gráficos

## 🧪 Testes Realizados

### Funcionalidades Testadas
- ✅ **CRUD completo** - Criar, ler, atualizar e excluir EPIs
- ✅ **Filtros** - Busca por nome e CA
- ✅ **Validações** - Campos obrigatórios e formatos
- ✅ **Dashboard** - Métricas e gráficos
- ✅ **Responsividade** - Interface adaptável
- ✅ **API REST** - Todos os endpoints funcionando

### Dados de Exemplo
O sistema inclui dados de exemplo para demonstração:
- Capacete de Segurança (válido)
- Óculos de Proteção (próximo do vencimento)
- Luvas de Segurança (vencido)
- Botina de Segurança (válido)

## 🚀 Deploy e Produção

### Configurações para Produção
1. **Alterar string de conexão** para banco de produção
2. **Configurar HTTPS** obrigatório
3. **Implementar autenticação** se necessário
4. **Configurar logs** estruturados
5. **Implementar cache** para melhor performance

### Melhorias Futuras
- **Autenticação e autorização** de usuários
- **Histórico de alterações** nos EPIs
- **Notificações** de vencimento por email
- **Relatórios** em PDF/Excel
- **API de integração** com outros sistemas
- **Backup automático** do banco de dados

## 📝 Licença

Este projeto foi desenvolvido como parte de um teste técnico e está disponível para fins educacionais e de demonstração.

## 👨‍💻 Desenvolvedor

Desenvolvido com ❤️ usando .NET 8.0 e as melhores práticas de desenvolvimento.

