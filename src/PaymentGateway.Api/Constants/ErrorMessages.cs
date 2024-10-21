namespace PaymentsGateway.Api.Constants
{
    public static class ErrorMessages
    {
        public const string InvalidParameters = "Invalid parameters";
        public const string InvalidParametersDetail = "The request was invalid";
        public const string InvalidParametersType = "invalid-parameters";

        public const string InternalServerErrorDetail = "Something went wrong, please try again later";
        public const string InternalServerErrorType = "internal-server-error";
        public const string InternalServerError = "Internal server error";

        public const string PaymentNotFoundErrorDetail = "Payment ID must exists";
        public const string PaymentNotFoundType = "payment-not-found";
        public const string PaymentNotFound = "Payment not found";

        public const string IdempotencyKeyError = "IdempotencyKey header missing.";

        public const string IdempotencyKeyConflictErrorType = "IdempotencyKey-concurrency-error";
        public const string IdempotencyKeyConflictErrorTitle = "IdempotencyKey concurrency conflict";
        public const string IdempotencyKeyConflictError = "The IdempotencyKey already used in another inprogress request.";

        public const string IdempotencyKeyReuseErrorType = "IdempotencyKey-reuse-error";
        public const string IdempotencyKeyReuseErrorTitle = "IdempotencyKey reuse";
        public const string IdempotencyKeyReuseError = "The IdempotencyKey used for another request.";
    }
}
