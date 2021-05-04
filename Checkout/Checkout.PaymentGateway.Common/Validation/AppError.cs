namespace Checkout.PaymentGateway.Common.Validation
{
    public class AppError
    {
        public readonly string Message;
        public readonly AppErrorLevelEnum Level;
        public readonly AppErrorEnum ErrorEnum;
        public AppError(AppErrorEnum errorEnum, AppErrorLevelEnum level, string message)
        {
            Message = message;
            Level = level;
            ErrorEnum = errorEnum;
        }

        public static AppError BadDataAccess = new AppError(AppErrorEnum.BadDataAccess,
                                                                        AppErrorLevelEnum.Business,
                                                                        "Could access data of failed result");

        public static AppError BadPaymentAmountValue = new AppError(AppErrorEnum.BadPaymentAmountValue, 
                                                                        AppErrorLevelEnum.Validation, 
                                                                        "Payment amount - bad value");

        public static AppError BadPaymentAmountValueFormat = new AppError(AppErrorEnum.BadPaymentAmountValueFormat,
                                                                        AppErrorLevelEnum.Validation,
                                                                        "Payment amount - bad format");

        public static AppError BadPaymentAmountValueValue = new AppError(AppErrorEnum.BadPaymentAmountValueValue,
                                                                        AppErrorLevelEnum.Validation,
                                                                        "Payment amount - bad value");

        public static AppError BadPaymentCardExpiryValue = new AppError(AppErrorEnum.BadPaymentCardExpiryValue,
                                                                        AppErrorLevelEnum.Validation,
                                                                        "Payment card expiry value - bad value");

        public static AppError BadPaymentCardExpiryValueFormat = new AppError(AppErrorEnum.BadPaymentCardExpiryValueFormat,
                                                                        AppErrorLevelEnum.Validation,
                                                                        "Payment card expiry value - bad format");

        public static AppError BadPaymentCardTypeValue = new AppError(AppErrorEnum.BadPaymentCardTypeValue,
                                                                        AppErrorLevelEnum.Validation,
                                                                        "Payment card type - bad value");

        public static AppError BadPaymentCardNumberValue = new AppError(AppErrorEnum.BadPaymentCardNumberValue,
                                                                        AppErrorLevelEnum.Validation,
                                                                        "Payment card number - bad value");

        public static AppError BadPaymentCardCvvValue = new AppError(AppErrorEnum.BadPaymentCardCvvValue,
                                                                        AppErrorLevelEnum.Validation,
                                                                        "Payment card Cvv - bad value");

        public static AppError BadCurrenyValue = new AppError(AppErrorEnum.BadCurrenyValue,
                                                                        AppErrorLevelEnum.Validation,
                                                                        "Currency - bad value");

        public static AppError MissingPaymentReference = new AppError(AppErrorEnum.MissingPaymentReference,
                                                                        AppErrorLevelEnum.Validation,
                                                                        "Payment Reference - missing");

        public static AppError BadPaymentStatusValue = new AppError(AppErrorEnum.BadPaymentStatusValue,
                                                                        AppErrorLevelEnum.Validation,
                                                                        "Payment Status - bad value");
    }

    public enum AppErrorEnum
    {
        BadDataAccess,
        CombinedError,
        BadPaymentAmountValue,
        BadPaymentAmountValueFormat,
        BadPaymentCardExpiryValue,
        BadPaymentCardExpiryValueFormat,
        BadPaymentCardTypeValue,
        BadPaymentCardNumberValue,
        BadPaymentCardCvvValue,
        BadCurrenyValue,
        MissingPaymentReference,
        BadPaymentStatusValue,
        BadPaymentAmountValueValue,
        PaymentNotFoundException
    }

    public enum AppErrorLevelEnum
    {
        Warn,
        Validation,
        Business,
        Fatal
    }
}
