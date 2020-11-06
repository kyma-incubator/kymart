# artexplorerfrontend

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