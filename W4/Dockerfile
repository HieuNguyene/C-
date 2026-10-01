# =======================================================
# Stage 1: Base Runtime (Chỉ chứa .NET 8 ASP.NET Runtime nhẹ, an toàn)
# =======================================================
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

# =======================================================
# Stage 2: Build (Dùng .NET 8 SDK để restore và build)
# =======================================================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Tận dụng Docker layer cache: Chỉ copy file csproj trước để restore
COPY ["W4.API/W4.API.csproj", "W4.API/"]
COPY ["W4.Application/W4.Application.csproj", "W4.Application/"]
COPY ["W4.Domain/W4.Domain.csproj", "W4.Domain/"]
COPY ["W4.Infrastructure/W4.Infrastructure.csproj", "W4.Infrastructure/"]

RUN dotnet restore "W4.API/W4.API.csproj"

# Copy toàn bộ source code còn lại và tiến hành build
COPY . .
WORKDIR "/src/W4.API"
RUN dotnet build "W4.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# =======================================================
# Stage 3: Publish (Đóng gói ứng dụng)
# =======================================================
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "W4.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# =======================================================
# Stage 4: Final Container (Image sản phẩm cuối cùng)
# =======================================================
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "W4.API.dll"]
