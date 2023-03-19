#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Tutorial.PhoneBook.WebApi/Tutorial.PhoneBook.WebApi.csproj", "Tutorial.PhoneBook.WebApi/"]
RUN dotnet restore "Tutorial.PhoneBook.WebApi/Tutorial.PhoneBook.WebApi.csproj"
COPY . .
WORKDIR "/src/Tutorial.PhoneBook.WebApi"
RUN dotnet build "Tutorial.PhoneBook.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Tutorial.PhoneBook.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Tutorial.PhoneBook.WebApi.dll"]