FROM node:16.13.2-alpine3.15 as builder

WORKDIR /builder
COPY ./src/Web/modules/admin ./

RUN yarn install --frozen-lockfile

RUN yarn build

FROM nginx:1.21.6-alpine

COPY --from=builder /builder/dist/index.html /usr/share/nginx/html/
COPY --from=builder /builder/dist/bundle.js /usr/share/nginx/html/

ENTRYPOINT ["nginx"]
CMD ["-g", "daemon off;"]