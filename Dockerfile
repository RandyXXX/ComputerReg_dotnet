#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ComputerReg/ComputerReg.csproj", "ComputerReg/"]
COPY ["DataLibrary/DataLibrary.csproj", "DataLibrary/"]
COPY ["AES/AES.csproj", "AES/"]
RUN dotnet restore "ComputerReg/ComputerReg.csproj"
COPY . .
WORKDIR "/src/ComputerReg"
RUN dotnet build "ComputerReg.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ComputerReg.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ComputerReg.dll"]