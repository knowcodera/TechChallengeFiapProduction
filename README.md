
# Production - Microsserviço

Este repositório contém o código-fonte do microsserviço **Production**, responsável por gerenciar o processo de produção de pedidos no sistema da lanchonete.

---

## 🔧 **Descrição**
O microsserviço **Production** gerencia:
- Acompanhar a fila de pedidos na cozinha.
- Atualização do status de cada passo do pedido.
- Comunicação com outros microsserviços como **Order** e **Payment**.

---

## 🚀 **Tecnologias**
Este projeto utiliza as seguintes tecnologias:
- **Linguagem:** C# com .NET 8.0
- **Banco de Dados:** Azure Cosmos DB (MongoDB API)
- **Mensageria:** RabbitMQ
- **Infraestrutura:** Azure Kubernetes Service (AKS), Azure Container Registry (ACR)
- **CI/CD:** GitHub Actions
- **Teste e Qualidade de Código:** SonarQube

---

## 🛠️ **Configuração**
### **Pré-requisitos**
1. **Infraestrutura**:
   - Azure AKS configurado.
   - Azure Container Registry configurado.
   - RabbitMQ rodando como serviço de mensageria.
   - Azure Cosmos DB configurado para o banco de dados **Production**.
2. **Ferramentas**:
   - Docker
   - Azure CLI
   - Terraform

---

## 🧪 **Testes**
Os testes do projeto foram implementados utilizando **xUnit** com cobertura mínima de 80%.

### **Evidência de Cobertura**
- **Screenshot da Cobertura de Testes**:

---

## 📦 **CI/CD**
O pipeline CI/CD está configurado no GitHub Actions:
- Realiza o build e testes automatizados.
- Publica a imagem Docker no Azure Container Registry.
- Faz o deploy no Azure Kubernetes Service (AKS).
- Faz o teste de cobertura via SonarQube com um mínimo de 70%.

### **Workflow Configurado**
Confira o workflow em:
```bash
.github/workflows/workflow.yml
```

---

## Licença
Este projeto está licenciado sob a licença MIT. Consulte o arquivo LICENSE para obter mais detalhes.
