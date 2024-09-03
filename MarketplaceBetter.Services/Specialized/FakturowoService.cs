using MarketplaceBetter.Domain.Model.Sales.Invoicing;
using MarketplaceBetter.Services.Specialized.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Specialized
{
    public class FakturowoService : IFakturowoService
    {
        private readonly HttpClient _client;
        private readonly string _apiId;
        private readonly Encoding _encoding;

        private readonly Dictionary<string, string> _currencyCodeToIdMapping = new Dictionary<string, string>
        {
            ["PLN"] = "0",
            ["EUR"] = "1",
            ["USD"] = "2",
            ["GBP"] = "3",
            ["CHF"] = "4",
            ["CAD"] = "5",
            ["AUD"] = "6",
            ["JPY"] = "7",
            ["THB"] = "8",
            ["HKD"] = "9",
            ["NZD"] = "10",
            ["SGD"] = "11",
            ["HUF"] = "12",
            ["UAH"] = "13",
            ["CZK"] = "14",
            ["DKK"] = "15",
            ["ISK"] = "16",
            ["NOK"] = "17",
            ["SEK"] = "18",
            ["HRK"] = "19",
            ["RON"] = "20",
            ["BGN"] = "21",
            ["TRY"] = "22",
            ["ILS"] = "23",
            ["CLP"] = "24",
            ["PHP"] = "25",
            ["MXN"] = "26",
            ["ZAR"] = "27",
            ["BRL"] = "28",
            ["MYR"] = "29",
            ["RUB"] = "30",
            ["IDR"] = "31",
            ["INR"] = "32",
            ["KRW"] = "33",
            ["CNY"] = "34",
            ["XDR"] = "35",
        };

        public FakturowoService()
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri(@"https://www.fakturowo.pl/api")
            };
            _apiId = "4a0278f9aec517771d5d659357d7e8d2";
            _encoding = Encoding.UTF8;
        }

        public async Task<bool> Issue(InvoiceModel invoice)
        {
            IList<KeyValuePair<string, string>> parameters = GetParameters(invoice);

            HttpContent content = ParametersToHttpContent(parameters);

            var response = await _client.PostAsync(string.Empty, content);

            var stream = await response.Content.ReadAsStreamAsync();

            using (var sr = new StreamReader(stream, Encoding.UTF8))
            {
                var status = await sr.ReadLineAsync();

                var apiNumberOrError = await sr.ReadLineAsync();

                if (status.Equals("1"))
                {
                    invoice.ApiError = null;
                    invoice.ApiNumber = apiNumberOrError;
                    invoice.IsIssued = true;
                }
                else
                {
                    invoice.ApiError = apiNumberOrError;

                    return false;
                }
            }

            return true;
        }

        private IList<KeyValuePair<string, string>> GetParameters(InvoiceModel invoice)
        {
            string nabywca_miasto;

            bool buyerCityValid = !string.IsNullOrEmpty(invoice.BuyerCity);
            bool buyerStateValid = !string.IsNullOrEmpty(invoice.BuyerState);

            if (buyerCityValid && buyerStateValid)
            {
                nabywca_miasto = $"{invoice.BuyerCity}, {invoice.BuyerState}";
            }
            else if (buyerCityValid && !buyerStateValid)
            {
                nabywca_miasto = invoice.BuyerCity;
            }
            else if (buyerStateValid)
            {
                nabywca_miasto = invoice.BuyerState;
            }
            else
            {
                nabywca_miasto = "Amazon Customer City";
            }

            List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>()
            {
                NewField("api_id", _apiId),
                NewField("api_zadanie", "1"),
                NewField("api_kodowanie", "0"),

                NewField("dokument_numer", invoice.Number),
                NewField("dokument_pokaz_numer", "1"),
                NewField("dokument_pokaz_data_w", "1"),
                NewField("dokument_data_s", invoice.PaymentDate.ToString("dd-MM-yyyy")),
                NewField("dokument_pokaz_data_s", "1"),
                NewField("dokument_data_s_oznaczenie", "6"),
                NewField("dokument_miejsce", "Radom"),
                NewField("dokument_pokaz_miejsce", "1"),
                NewField("dokument_rodzaj", "0"),
                NewField("dokument_oznaczenie", "0"),
                NewField("dokument_jezyk", "0"),
                NewField("dokument_drugi_jezyk", "2"),
                NewField("dokument_waluta", _currencyCodeToIdMapping[invoice.Entries.First().Currency.Name]),
                NewField("dokument_zaplata", "14"),
                NewField("dokument_pokaz_zaplata", "1"),
                NewField("dokument_status", "1"),
                NewField("dokument_zaplacono", (invoice.Entries.Sum(x => x.GrossPrice) + invoice.ShippingGrossPrice).ToString(System.Globalization.CultureInfo.InvariantCulture)),
                NewField("dokument_zaplacono_opis", ""),
                NewField("dokument_pokaz_zaplacono_opis", "0"),
                NewField("dokument_pokaz_data_p", "0"),
                NewField("dokument_zaplacono_oznaczenie", "0"),
                NewField("dokument_termin", ""),
                NewField("dokument_pokaz_termin", "0"),
                NewField("dokument_uwagi", invoice.Entries.Aggregate(new StringBuilder($"Amazon: {invoice.OrderId}"), (sb,e) => sb.Append(" " + e.OrderItemId)).ToString()),
                NewField("dokument_uwagi_oznaczenie", "1"),

                NewField("sprzedawca_nazwa", "Futrzane Andrzej Sadkowski"),
                NewField("sprzedawca_nip", invoice.VatRule.VatNumber),
                NewField("sprzedawca_ulica", "Młyńska 20A / 1"),
                NewField("sprzedawca_miasto", "Radom"),
                NewField("sprzedawca_kod", "26-600"),
                NewField("sprzedawca_panstwo", "Polska"),
                NewField("sprzedawca_opis", "Numer BDO: 000307021"),
                NewField("sprzedawca_pokaz_opis", "1"),
                NewField("sprzedawca_podpis", "Andrzej Sadkowski"),
                NewField("sprzedawca_pokaz_podpis", "1"),
                NewField("sprzedawca_oznaczenie", "0"),

                NewField("nabywca_osoba", "1"),
                NewField("nabywca_imie", invoice.BuyerFirstName),
                NewField("nabywca_nazwisko", invoice.BuyerLastName),
                NewField("nabywca_ulica", invoice.BuyerStreet),
                NewField("nabywca_miasto", nabywca_miasto),
                NewField("nabywca_kod", invoice.BuyerPostalCode),
                NewField("nabywca_panstwo", invoice.BuyerCountry),
                NewField("nabywca_pokaz_podpis", "0"),
                NewField("nabywca_oznaczenie", "1"),
            };

            if (invoice.ShippingGrossPrice > 0m)
            {
                parameters.AddRange(new[]
                {
                    NewField("produkt_nazwa_1", "Dostawa towaru / Delivery"),
                    NewField("produkt_jm_1", "1"),
                    NewField("produkt_ilosc_1", "1"),
                    NewField("produkt_wartosc_brutto_1", invoice.ShippingGrossPrice.ToString(System.Globalization.CultureInfo.InvariantCulture)),
                    NewField("produkt_stawka_vat_1", invoice.VatRule.VatValue.ToString().Replace(',', '.')),
                });

                int entryNumber = 2;

                foreach (var entry in invoice.Entries)
                {
                    parameters.AddRange(new[]
                    {
                        NewField($"produkt_nazwa_{entryNumber}", entry.InvoiceName),
                        NewField($"produkt_jm_{entryNumber}", "2"),
                        NewField($"produkt_ilosc_{entryNumber}", entry.Quantity.ToString()),
                        NewField($"produkt_wartosc_brutto_{entryNumber}", entry.GrossPrice.ToString(System.Globalization.CultureInfo.InvariantCulture)),
                        NewField($"produkt_stawka_vat_{entryNumber}", invoice.VatRule.VatValue.ToString().Replace(',', '.'))
                    });

                    entryNumber++;
                }
            }
            else
            {
                int entryNumber = 1;

                foreach (var entry in invoice.Entries)
                {
                    parameters.AddRange(new[]
                    {
                        NewField($"produkt_nazwa_{entryNumber}", entry.InvoiceName),
                        NewField($"produkt_jm_{entryNumber}", "2"),
                        NewField($"produkt_ilosc_{entryNumber}", entry.Quantity.ToString()),
                        NewField($"produkt_wartosc_brutto_{entryNumber}", entry.GrossPrice.ToString(System.Globalization.CultureInfo.InvariantCulture)),
                        NewField($"produkt_stawka_vat_{entryNumber}", invoice.VatRule.VatValue.ToString().Replace(',', '.'))
                    });

                    entryNumber++;
                }
            }

            return parameters;
        }

        private HttpContent ParametersToHttpContent(IList<KeyValuePair<string, string>> parameters)
        {
            var urlEncodedString = parameters.Skip(1).Aggregate(new StringBuilder(parameters.First().Key + "=" + parameters.First().Value), (sb, x) => sb.Append("&" + x.Key + "=" + x.Value)).ToString();

            var content = new ByteArrayContent(_encoding.GetBytes(urlEncodedString));

            content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

            return content;
        }

        private static KeyValuePair<string, string> NewField(string fieldName, string fieldValue)
        {
            return new KeyValuePair<string, string>(fieldName, fieldValue);
        }
    }
}
