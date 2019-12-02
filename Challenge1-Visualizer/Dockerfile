FROM node:10
WORKDIR /app
COPY package*.json ./
RUN npm install
COPY . .
EXPOSE 3000
EXPOSE 8000
EXPOSE 6379
ENTRYPOINT ["npm", "run","dev"]