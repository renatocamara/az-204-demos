FROM node:10

# Create app directory
WORKDIR /usr/src/app

# Install app dependencies
# A wildcard is used to ensure both package.json AND package-lock.json are copied
# where available (npm@5+)
COPY package*.json ./

# This command installs a package, and any packages that it depends on.
RUN npm install
# If you are building your code for production
# RUN npm ci --only=production

# Bundle app source
# copy files and directories inside a Docker Container from your Local machine
COPY . .

EXPOSE 9090
CMD [ "node", "server.js" ]