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

2. Open the solution directly in Visual Studio and run the PaymentsGateway.API to run the API or You can build the API from the command line

Build and run (from the project directory):
```sh
$ dotnet build
$ dotnet run --project .\PaymentsGateway.Api\PaymentsGateway.Api.csproj
```

3. You can run the Tests from Test-->Run All Test Menu from Visual studio or run below command in terminal.

```sh
$ dotnet test
```

Assumptions

- The `authorization_code` is not part of the API response, but I assume we need it later, so it is stored in the repository for future processing needs.

## Areas for Improvements

- Introduce JWT Authentication and request signing to secure the application.
- I would implement custom Idempotency in real time. To avoid creating multiple payments for the same request. This could be achieved by sending an idempotency key as a request header. 
- As part of service observability, I would improve exception handling and logging to debug errors quickly. I would use Elastic search to store logs for quicker search and visualisations.
- As part of service observability, I would create Graphana dashbords to monitor service health metrics.
- Implementing resilience mechanisms by introducing [Polly](https://github.com/App-vNext/Polly#polly).
- I would use Honeycomb tool to trace service requests.
- I would implement rate limiting on public endpoints to limit the number of requests.
- I would implement end to end tests on public endpoints to identify intermittent issues in all environments.
- I would run load tests on service to capture application peformance metrics.
- I would add more unit and integration tests to cover all parts of source code.
- Ensure that persisted PCI and PII information is securely encrypted.
- Ensure that any PCI and PII information logged is masked.