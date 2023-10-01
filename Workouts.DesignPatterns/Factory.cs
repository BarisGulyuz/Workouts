namespace Workouts.DesignPatterns
{
    /// <summary>
    ///Simple
    /// </summary>
    public class Factory
    {
        public enum CreaditCardType
        {
            Silver,
            Gold,
            Platinum
        }

        public interface ICreditCard
        {
            string CardName { get; }

            int GetAnnualCharge();
            int GetCreditLimit();
            string GetFullInfo();
        }

        public class CreditCardFactory
        {
            public static ICreditCard Create(CreaditCardType creaditCardType)
            {
                return creaditCardType switch
                {
                    CreaditCardType.Silver => new SilverCard(),
                    CreaditCardType.Gold => new GoldCard(),
                    CreaditCardType.Platinum => new PlatinumCard(),
                    _ => throw new NotImplementedException()
                };
            }
        }

        internal class SilverCard : ICreditCard
        {
            public string CardName => nameof(CreaditCardType.Silver);

            public int GetAnnualCharge() => 100;

            public int GetCreditLimit() => 1000;

            public string GetFullInfo() => $"{CardName} - {GetCreditLimit()} - {GetCreditLimit()}";
        }
        internal class GoldCard : ICreditCard
        {
            public string CardName => nameof(CreaditCardType.Gold);

            public int GetAnnualCharge() => 100;

            public int GetCreditLimit() => 2000;

            public string GetFullInfo() => $"{CardName} - {GetCreditLimit()} - {GetCreditLimit()}";
        }
        internal class PlatinumCard : ICreditCard
        {
            public string CardName => nameof(CreaditCardType.Platinum);

            public int GetAnnualCharge() => 100;

            public int GetCreditLimit() => 3000;

            public string GetFullInfo() => $"{CardName} - {GetCreditLimit()} - {GetCreditLimit()}";
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class FactoryMethod
    {
        public enum PaymentType
        {
            Havale,
            EFT,
            FAST
        }
        public interface IPayment
        {
            bool DoValidation();

            void DoAccounting();

            void FinishPayment();
        }
        internal class EFT : IPayment
        {
            public void DoAccounting()
            {
                Console.WriteLine("EFT Accounting Done");
            }

            public bool DoValidation()
            {
                return true;
            }

            public void FinishPayment()
            {
                Console.WriteLine("EFT Payment Saved And Sent");
            }
        }
        internal class FAST : IPayment
        {
            public void DoAccounting()
            {
                Console.WriteLine("FAST Accounting Done");
            }

            public bool DoValidation()
            {
                return true;
            }

            public void FinishPayment()
            {
                Console.WriteLine("FAST Payment Saved And Sent");
            }
        }
        internal class Havale : IPayment
        {
            public void DoAccounting()
            {
                Console.WriteLine("Havele Accounting Done");
            }

            public bool DoValidation()
            {
                return true;
            }

            public void FinishPayment()
            {
                Console.WriteLine("Havele Payment Saved And Sent");
            }
        }
        public abstract class PaymentManager
        {
            internal abstract IPayment Create(PaymentType paymentType);

            public void DoPayment(PaymentType paymentTyp)
            {
                IPayment payment = Create(paymentTyp);

                payment.DoValidation();
                payment.DoAccounting();
                payment.FinishPayment();

                //bla bla bla
            }
        }
        public class InterbankPaymentManager : PaymentManager
        {
            public InterbankPaymentManager()
            {
            }

            internal override IPayment Create(PaymentType paymentTyp)
            {
                return paymentTyp switch
                {
                    PaymentType.EFT => new EFT(),
                    PaymentType.FAST => new FAST(),
                    _ => throw new InvalidOperationException()
                };
            }
        }
        public class IntrabankPaymentManager : PaymentManager
        {
            public IntrabankPaymentManager()
            {
            }

            internal override IPayment Create(PaymentType paymentType)
            {
                return paymentType switch
                {
                    PaymentType.Havale => new Havale(),
                    _ => throw new InvalidOperationException()
                };
            }
        }
    }
}

