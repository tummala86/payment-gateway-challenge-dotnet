# PaymentGateway
The service hosts the following endpoints.

```
-- Health Check --
GET /payments/up

--  Payments --
POST /payments
GET /payments/{paymentId}
```

🚀 **POST /payments**

**Request:**
```console
curl --location --request POST 'http://localhost:5067/payments' \
--header 'Content-Type: application/json' \
--data-raw '{
    "cardnumber": "2222405343248877",
    "expirymonth": 4,
    "expiryyear": 2025,
    "amount": 100,
    "currency": "GBP",
    "cvv": "123"
}
'
```
**Response:**
```json
{
    "id": "69fadc87-dfda-47e8-84a1-74aeaecaa92a",
    "status": "Authorized",
    "cardNumberLastFour": "8877",
    "expiryMonth": 4,
    "expiryYear": 2025,
    "currency": "GBP",
    "amount": 100
}
```

🚀 **GET /payments/{paymentId}**

**Request:**
```console
curl --location --request GET 'http://localhost:5067/payments/c886043c-47ae-40b3-b958-8cec9a4932cd' 
```
**Response:**
```json
{
    "id": "69fadc87-dfda-47e8-84a1-74aeaecaa92a",
    "status": "Authorized",
    "cardNumberLastFour": "8877",
    "expiryMonth": 4,
    "expiryYear": 2025,
    "currency": "GBP",
    "amount": 100
}
```

## Prerequisites

This project relies on the following dependencies:

* [.NET 8](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
* [Entity Framework Core](https://learn.microsoft.com/en-us/ef/core/providers/in-memory/?tabs=dotnet-core-cli)
* [Docker](https://www.docker.com/) (optional, required for running the project in Docker).

## Clone Instructions
Clone the code into your local using the below command:
Clone the project:
```sh
$ git clone https://github.com/tummala86/payment-gateway-challenge-dotnet.git
```

There are two ways to run this project:

1. Start the simulator 

```sh
$ run docker-compose up
```

2. Open the solution directly in Visual Studio and run the PaymentGateway.API to start the API, or build and run the API from the command line.

Build and run (from the project directory):
```sh
$ dotnet build
$ dotnet run --project .\PaymentGateway.Api\PaymentGateway.Api.csproj
```

3. You can run the tests from the `Test` > `Run All Tests` menu in Visual Studio, or by executing the following command in the terminal.

```sh
$ dotnet test
```

Assumptions

- The `authorization_code` is not part of the API response, but I assume we need it later, so it is stored in the repository for future processing needs.

## Areas for Improvements

- Introduce JWT Authentication and request signing to secure the application.
- I will implement custom idempotency in real-time to prevent creating multiple payments for the same request. This can be achieved by sending an idempotency key as a request header. 
- To enhance service observability, I will improve exception handling and logging to facilitate quick error debugging. I will use Elasticsearch to store logs for faster search and visualization.
- I will create Graphana dashbords to monitor service health metrics.
- Implementing resilience mechanisms by introducing [Polly](https://github.com/App-vNext/Polly#polly).
- I will use Honeycomb tool to trace service requests.
- I will implement rate limiting on public endpoints to limit the number of requests.
- I will implement end to end tests on public endpoints to identify intermittent issues in all environments.
- I will run load tests on service to capture application peformance metrics.
- I will add more unit and integration tests to cover all parts of source code.
- Ensure that persisted PCI and PII information is securely encrypted.
- Ensure that any PCI and PII information logged is masked.