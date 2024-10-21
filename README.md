# PaymentsGateway
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
--header 'IdempotencyKey: test3748335554ee4' \
--header 'Content-Type: application/json' \
--data-raw '{
    "card_number": "2222405343248877",
    "expiry_month": 4,
    "expiry_year": 2025,
    "amount": 100,
    "currency": "GBP",
    "cvv": "123"
}
'
```
**Response:**
```json
{
    "id": "c886043c-47ae-40b3-b958-8cec9a4932cd",
    "status": "Authorized",
    "card_number": "8877",
    "expiry_month": 4,
    "expiry_year": 2025,
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
    "id": "c886043c-47ae-40b3-b958-8cec9a4932cd",
    "status": "Authorized",
    "card_number": "8877",
    "expiry_month": 4,
    "expiry_year": 2025,
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
1. You could open the solution directly in Visual Studio and run the PaymentsGateway.API to run the API. You can run the Tests from Test-->Run All Test Menu.


2. You can build the API from the command line
Build and run (from the project directory):
```sh
$ dotnet build
$ dotnet run --project .\PaymentsGateway.Api\PaymentsGateway.Api.csproj
```

To run tests:
```sh
$ dotnet test
```

## Areas for Improvements

- Introduce JWT Authentication and request signing to secure the application.
- I would implement custom Idempotency in real time. Currently using opensource IdempotentAPI to achieve the test functionality. 
- Improve exception handling and logging to trace the issues quickly. I would use Elastic search to store logs for quicker search and visualisations.
- Consider event-driven architecture for asyncronous communication with banks. I would use webhooks to send payment status updates to clients.
- I would create Graphana dashbords to monitor service health.
- Implementing resilience mechanisms by introducing [Polly](https://github.com/App-vNext/Polly#polly).
- I would use Honeycomb tool to trace service requests.
- I would implement rate limiting on public endpoints to limit the number of requests.
- I would implement end to end tests on public endpoints to identify intermittent issues in all environments.
- I would run load tests on service to capture application peformance metrics.
- I would add more unit and integration tests to cover all parts of source code.
- Ensure that persisted PCI and PII information is securely encrypted.
- Ensure that any PCI and PII information logged is masked.