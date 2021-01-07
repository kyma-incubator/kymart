# Kymart

Create Namespace:
```
kubectl apply -f namespace.yaml
```

To Deploy :

```
kubectl apply -k .
```

To Delete:

```
kubectl delete -k .
```


## Deploying a new module

1. Use `docker login` to access Github Packages. To find out more about logging in, visit [Packages docs](https://docs.github.com/en/free-pro-team@latest/packages/guides/configuring-docker-for-use-with-github-packages).

```bash
cat personal-token.txt | docker login https://ghcr.io -u <username> --password-stdin
```

2. Push your built image

```bash
docker push ghcr.io/kyma-incubator/kymart/<PACKAGE_NAME>:latest
```

3. By default, your package will be private. To fix that, go to the [packages list](https://github.com/orgs/kyma-incubator/packages) and make it public.
3.1 Enter `Package Settings`
3.2 Choose `Make public` and confirm.

From now on your image should be accessible for pulling.
