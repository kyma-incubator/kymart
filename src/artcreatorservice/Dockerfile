# build stage
FROM node:lts-alpine as build-stage
WORKDIR /app
COPY package*.json yarn.lock ./
RUN yarn install
COPY . .
EXPOSE 8080
HEALTHCHECK --interval=30s --timeout=30s --start-period=5s --retries=3 CMD [ "node", "healthcheck.js" ]
CMD ["node", "index.js"]