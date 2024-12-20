# VerticleSlices

## Description

VerticalSlices is a .NET Core project that demonstrates the implementation of vertical slice architecture. This approach enhances maintainability, testability, and scalability by organizing code by feature rather than technical concern

## Features

- Feature-Based Structure: Organizes the application by features, each having its own directory.
- Decoupled Architecture: Promotes modularity and reduces dependencies.
- Command and Query Separation**: Clear separation of commands (write operations) and queries (read operations).
- Modular Structure: Each feature is encapsulated within its own module, enhancing modularity.
- MediatR: Utilizes MediatR for handling commands and queries, promoting a clean separation of concerns.
- Entity Framework Core: Leverages EF Core for data access, ensuring robust and efficient database interactions.
- Improved Testability: Allows testing of individual slices independently.
- Scalability: Facilitates easy addition and modification of features.

## Technologies
- ASP.NET Core
- Entity Framework Core
- PostgreSQL
- Docker

## Getting Started
### Prerequisites
To run this project locally, you'll need to have the following installed:
- .NET Core SDK
- [Docker](https://www.docker.com/get-started)
- [Docker Compose](https://docs.docker.com/compose/install/)
- PostgreSQL

## Installation

1. Clone this repository:
   ```bash
   git clone https://github.com/ajaybhalerao12/VerticleSlices.git
2. Navigate to the project directory:
   ```bash   
   cd VerticleSlices
3. Build and run the Docker containers:
   ```bash   
   docker-compose up --build

## Usage

### New Endpoints

The following new endpoints have been added for interacting with articles:

#### **Create Article**
- **URL**: `/api/articles`
- **Method**: POST
- **Description**: Creates a new article.
- **Request Body**:
    ```json
    {
        "Title": "Sample Title",
        "Content": "Sample content for the article",
        "Tags": ["tag1", "tag2"]
    }
    ```
- **Response**:
    ```json
    {
        "id": "guid",
        "title": "Sample Title",
        "content": "Sample content for the article",
        "tags": ["tag1", "tag2"],
        "createdOnUtc": "2024-12-20T08:30:00Z"
    }
    ```

#### **Get Article by ID**
- **URL**: `/api/articles/{id}`
- **Method**: GET
- **Description**: Retrieves an article by its ID.
- **Response**:
    ```json
    {
        "id": "guid",
        "title": "Sample Title",
        "content": "Sample content for the article",
        "tags": ["tag1", "tag2"],
        "createdOnUtc": "2024-12-20T08:30:00Z"
    }
    ```

#### **Update Article**
- **URL**: `/api/articles/{id}`
- **Method**: PUT
- **Description**: Updates an existing article by its ID.
- **Request Body**:
    ```json
    {
        "Id": "guid",
        "Title": "Updated Title",
        "Content": "Updated content for the article",
        "Tags": ["updatedTag1", "updatedTag2"]
    }
    ```
- **Response**: No content (204).

#### **Delete Article**
- **URL**: `/api/articles/{id}`
- **Method**: DELETE
- **Description**: Deletes an article by its ID.
- **Response**: No content (204).


### Contributing
Contributions are welcome! Fork this repository and submit a pull request for any enhancements or bug fixes.


## License
This project is licensed under the MIT License. See the LICENSE file for more details.
```bash
Feel free to customize the content further based on your project's specific details and requirements.
```
## Contact
For any questions or suggestions, please contact [Ajay Bhalerao](https://github.com/ajaybhalerao12).



