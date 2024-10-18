# Documentacão


O projeto consiste em 2 solutions, uma pra API (WebApi) e uma para o APP (BlazorApp)

Separei os 2 pois pensava no step de deploy e achei que ia ficar mais simples


Parei nos tests que comecei a ver o terraform, achei que ia tomar mais do meu tempo os estudos disso, apenas criei os projetos de tests, mas não dei andamento.

## Dependencias
Unicas depêndencias: 
* Gerenciar as credencias de acesso no servidor. Projeto da API na execução vai gerar 2 tabelas no DynamoBd **User** e **Favorite**
* Credências da API publica (https://openweathermap.org/api), não subi as minhas
* Parametrização do Token

Tudo vai ser suprido no appsettings (meu de dev eu nao subi pelas minhas credênciais)

Ambos os projetos é só rodar e verificar no APP (Blazor) se o serviço da API está apontando na porta certa
