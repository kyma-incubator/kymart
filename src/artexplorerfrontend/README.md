# artexplorerfrontend

## Local development

To run the project locally, you'll need to setup a OAuth 2.0 client ID.

1. Go to https://console.developers.google.com/apis/credentials
1. `Create Credentials` -> `OAuth Client ID`
1. `Application Type`: `Web application`
`Authorized Javascript origins`: `http://localhost:8080`
`Authorized redirect URIs`: `http://localhost:8080/oidc-callback`

After creating a client, duplicate the [.env.production](.env.production) file and rename it to `.env.local`. Vue will use it for configuration. Note that this file is ignored by git.

It's time to update your local configuration in `.env.local`:

1. Set `clientId` to your client ID (from client details page).
1. Replace all occurences of `https://art-explorer.kyma.fun` with `http://localhost:8080`.

You should be good to go, it's time to run the project.

## Project setup
```
yarn install
```

### Compiles and hot-reloads for development
```
yarn serve
```

### Compiles and minifies for production
```
yarn build
```

### Lints and fixes files
```
yarn lint
```

### Customize configuration
See [Configuration Reference](https://cli.vuejs.org/config/).

### Build Docker image

```
docker build -t artexplorerfrontend .
```

### Run Docker image

```
docker run -it -p 8080:80 --rm --name artexplorerfrontend artexplorerfrontend:latest
```