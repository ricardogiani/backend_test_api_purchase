
# Criar novas migrations usando o settings do outro projeto
dotnet ef migrations add InitialCreate --project Ambev.DeveloperEvaluation.ORM --startup-project Ambev.DeveloperEvaluation.WebApi

# Aplicar Migration usando o settings do outro projeto
dotnet ef database update --project Ambev.DeveloperEvaluation.ORM --startup-project Ambev.DeveloperEvaluation.WebApi
