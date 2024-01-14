#!/bin/bash

set -e

CONTAINER_NAME="recipes_cont"
IMAGE_NAME="krahristov/portfolio:latest"
APP_DIR="$HOME/portfolio-backend"

echo "Starting deployment..."

echo "Pulling the Docker image..."
sudo docker pull $IMAGE_NAME

echo "Stopping the existing Docker container..."
sudo docker stop $CONTAINER_NAME || true
sudo docker rm $CONTAINER_NAME || true

echo "Running the new Docker container..."
sudo docker run -p 8080:8080 -d --name $CONTAINER_NAME $IMAGE_NAME

echo "Removing unused Docker images..."
sudo docker image prune -af

echo "Deployment successful!"
