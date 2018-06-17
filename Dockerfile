# Create a temporary image for running our build
FROM microsoft/dotnet:2.1-sdk-alpine AS build
WORKDIR /app

# Copy the project files only, and restore dependencies
COPY *.sln */*.csproj ./
RUN perl -e 'foreach(@ARGV){/(.*).csproj/;mkdir($1);rename($&,"$1/$&")}' *.csproj
RUN dotnet restore

# Copy and build everything
COPY . .
RUN dotnet build --no-restore -c Release

# Bundle up the specific application we're after
ARG app
RUN dotnet publish --no-restore --no-dependencies -c Release -o ../out Fantabulous.${app}/*.csproj
RUN echo exec dotnet Fantabulous.${app}.dll > out/entrypoint.sh


# Create our actual output image
FROM microsoft/doptnet:2.1-aspnetcore-runtime-alpine
WORKDIR /app

# Copy the app across from the build image
COPY --from=build /app/out .

# Set the application entrypoint
ENTRYPOINT ["sh", "entrypoint.sh"]
