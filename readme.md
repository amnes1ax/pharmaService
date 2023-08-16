# Pharmacy Management System
This repository contains a web application and an API for a pharmaceutical company. The project is designed to manage various aspects of the pharmaceutical business, including products, pharmacies, warehouses, and batches.

# Technologies Used
* Backend: .NET 7 / C#
* Frontend: Blazor
* Database: PostgreSQL
* Deployment: Docker

# Features
### Products
Create Product: Effortlessly add new products to the system, inputting critical details such as product ID and name.

Delete Product: Seamlessly remove a product from the system, including all batches related to that product across all pharmacies.

### Pharmacies
Create Pharmacy: Easily create pharmacy records, including pharmacy ID, name, address, and phone number.

Delete Pharmacy: Effortlessly delete a pharmacy, removing it from the system along with associated warehouses and batches.

### Warehouses
Create Warehouse: Quickly add warehouses to the system, specifying warehouse ID, associated pharmacy ID, and name.

Delete Warehouse: Smoothly delete a warehouse, along with all batch-related data stored within that warehouse.

### Batches
Create Batch: Instantly generate batch records, indicating batch ID, associated product ID, warehouse ID, and batch quantity.

Delete Batch: Effortlessly delete a batch record from the system.

### Client Interface
The client interface provides an array of functionalities:

List of Products for Selected Pharmacy: Displays available products at the chosen pharmacy.

List of Pharmacies with Product Listing: Offers a list of pharmacies, enabling users to view products available at each pharmacy.

List of Warehouses with Batch Listing: Presents warehouses and their associated batches. Users can explore batches within a selected warehouse.

List of Batches for Selected Warehouse: Displays batches stored at the chosen warehouse.

# Installation
1. Clone this repository to your local machine.

2. Configure the PostgreSQL database for the backend.

3. Set up the Blazer front end application using .NET 7.

4. Utilize Docker for seamless installation and deployment.

    - Install Docker on your machine if you haven't already.
    - Navigate to the project root directory in your terminal.
    - Run the following command to start the project using Docker Compose:
    
      ```bash
      docker-compose up -d
      ```
    
    - This will build and start the application containers. You can access the application by navigating to http://localhost in your web browser.
	
# Contributors
* Alexey Ionov

## Future Improvements

1. **Expanding Entities**: Extend existing entities like products, pharmacies, warehouses, and batches to include more relevant attributes, offering deeper insights and control over your pharmaceutical operations.

2. **Entity Editing**: Implement functionality to edit existing entities, allowing for updates and corrections to be made seamlessly within the system.

3. **Business Logic Enhancement**: Incorporate more complex business logic into the system, such as inventory management, order fulfillment, and supply chain optimization.

4. **Paged Lists**: Replace the current list displays with paginated lists to improve user experience when working with a large number of entities.

5. **Authentication and Authorization**: Integrate authentication and authorization mechanisms to ensure secure access to different sections of the application and protect sensitive data.

6. **Test Coverage**: Enhance test coverage by implementing unit tests, integration tests, and end-to-end tests. This will ensure the reliability and stability of the application as it evolves.

7. **CQRS and Microservices**: Consider transitioning to a CQRS (Command Query Responsibility Segregation) architecture and breaking down the application into microservices to achieve greater scalability and maintainability.

8. **Enhanced User Interface**: Continuously improve the user interface by incorporating modern design principles and enhancing the user experience.

These are just a few potential directions for improving and expanding the Pharmacy Management System. As the project evolves, incorporating these enhancements can lead to a more robust and feature-rich solution that meets the evolving needs of the pharmaceutical industry.