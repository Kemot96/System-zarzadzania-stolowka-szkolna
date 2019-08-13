using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Projekt.Models;
using Microsoft.AspNet.Identity;
using PayPal.Api;

namespace Projekt.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        private StolowkaEntities db = new StolowkaEntities();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SuccessView()
        {
            return View();
        }

        public ActionResult FailureView()
        {
            return View();
        }

        public ActionResult Dzieci()
        {         
            string currentMonth = DateTime.Now.Month.ToString();

            string currentYear = DateTime.Now.Year.ToString();

            if (currentMonth == "1")
            {
                currentMonth = "Styczen";
            }
            else if (currentMonth == "2")
            {
                currentMonth = "Luty";
            }
            else if (currentMonth == "3")
            {
                currentMonth = "Marzec";
            }
            else if (currentMonth == "4")
            {
                currentMonth = "Kwiecien";
            }
            else if (currentMonth == "5")
            {
                currentMonth = "Maj";
            }
            else if (currentMonth == "6")
            {
                currentMonth = "Czerwiec";
            }
            else if (currentMonth == "7")
            {
                currentMonth = "Lipiec";
            }
            else if (currentMonth == "8")
            {
                currentMonth = "Sierpien";
            }
            else if (currentMonth == "9")
            {
                currentMonth = "Wrzesien";
            }
            else if (currentMonth == "10")
            {
                currentMonth = "Pazdziernik";
            }
            else if (currentMonth == "11")
            {
                currentMonth = "Listopad";
            }
            else if (currentMonth == "12")
            {
                currentMonth = "Grudzien";
            }

            var dziecko = db.Dziecko.Include(d => d.Adres).Include(d => d.AspNetUsers);

            var miesiac = db.Miesiac.First(u => u.nazwa == currentMonth);

            var rok = db.Rok.First(u => u.rok1 == currentYear);

            var oplata = db.Oplata.Where(x => x.miesiac_id == miesiac.id).Where(x => x.rok_id == rok.id);

            var oneOplata = oplata.FirstOrDefault();

            var transakcja = db.Transakcja.Where(x => x.oplata_id == oneOplata.id).ToList();

            var view = new Transakcja_Oplata()
            {
                Transakcja = transakcja,
                Oplata = oplata,
                Dziecko = dziecko
            };
            return View(view);
        }

        public ActionResult Kontakt()
        {
            try
            {
                var pracownik = db.Pracownik.Where(u => u.stanowisko == "Kierownik").ToList();
                var firma = db.Firma.ToList();

                var view = new Firma_Pracownik()
                {
                    Firma = firma,
                    Pracownik = pracownik
                };

                return View(view);
            }
            catch (Exception ex)
            {
                return View("FailureView");
            }


        }

        public ActionResult Jadlospis()
        {
            try
            {
                string currentMonth = DateTime.Now.Month.ToString();

                string currentYear = DateTime.Now.Year.ToString();

                if (currentMonth == "1")
                {
                    currentMonth = "Styczen";
                }
                else if (currentMonth == "2")
                {
                    currentMonth = "Luty";
                }
                else if (currentMonth == "3")
                {
                    currentMonth = "Marzec";
                }
                else if (currentMonth == "4")
                {
                    currentMonth = "Kwiecien";
                }
                else if (currentMonth == "5")
                {
                    currentMonth = "Maj";
                }
                else if (currentMonth == "6")
                {
                    currentMonth = "Czerwiec";
                }
                else if (currentMonth == "7")
                {
                    currentMonth = "Lipiec";
                }
                else if (currentMonth == "8")
                {
                    currentMonth = "Sierpien";
                }
                else if (currentMonth == "9")
                {
                    currentMonth = "Wrzesien";
                }
                else if (currentMonth == "10")
                {
                    currentMonth = "Pazdziernik";
                }
                else if (currentMonth == "11")
                {
                    currentMonth = "Listopad";
                }
                else if (currentMonth == "12")
                {
                    currentMonth = "Grudzien";
                }

                var jadlospis = db.Jadlospis.Where(u => u.od <= DateTime.Now).Where(u => u.@do >= DateTime.Now).ToList();

                var miesiac = db.Miesiac.First(u => u.nazwa == currentMonth);

                var rok = db.Rok.First(u => u.rok1 == currentYear);

                var oplata = db.Oplata.Where(x => x.miesiac_id == miesiac.id).Where(x => x.rok_id == rok.id);

                var view = new Jadlospis_Oplata()
                {
                    Jadlospis = jadlospis,
                    Oplata = oplata
                };

                ViewBag.currentMonth = currentMonth;
                ViewBag.currentYear = currentYear;

                return View(view);
            }
            catch (Exception ex)
            {
                return View("FailureView");
            }


        }

        [Authorize(Roles = "uzytkownik")]
        public ActionResult Platnosci()
        {
            try
            {
                var user = User.Identity.GetUserId();

                var dziecko = db.Dziecko.First(u => u.konto_id == user);

                var transakcja = db.Transakcja.Where(x => x.dziecko_id == dziecko.id).ToList();

                return View(transakcja);
            }
            catch (Exception ex)
            {
                return View("FailureView");
            }

        }

        public ActionResult Nieobecnosci()
        {
            try
            {
                var user = User.Identity.GetUserId();

                var dziecko = db.Dziecko.First(u => u.konto_id == user);

                var nieobecnosc = db.Nieobecnosc.Where(x => x.dziecko_id == dziecko.id).ToList();

                return View(nieobecnosc);
            }
            catch (Exception ex)
            {
                return View("FailureView");
            }


        }

        public ActionResult DodajNieobecnosc()
        {
            return View();
        }

        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DodajNieobecnosc([Bind(Include = "id,od,do")] Nieobecnosc nieobecnosc)
        {
            try
            {
                var user = User.Identity.GetUserId();
                var dziecko = db.Dziecko.First(u => u.konto_id == user);


                if (ModelState.IsValid)
                {
                    db.Nieobecnosc.Add(new Nieobecnosc
                    {
                        id = nieobecnosc.id,
                        od = nieobecnosc.od,
                        @do = nieobecnosc.@do,
                        dziecko_id = dziecko.id
                    });
                    db.SaveChanges();
                    return RedirectToAction("Nieobecnosci");
                }

                return View(nieobecnosc);
            }
            catch (Exception ex)
            {
                return View("FailureView");
            }

        }

        // GET: Nieobecnosc/Delete/5
        public ActionResult UsunNieobecnosc(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nieobecnosc nieobecnosc = db.Nieobecnosc.Find(id);
            if (nieobecnosc == null)
            {
                return HttpNotFound();
            }
            return View(nieobecnosc);
        }

        // POST: Nieobecnosc/Delete/5
        [HttpPost, ActionName("UsunNieobecnosc")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Nieobecnosc nieobecnosc = db.Nieobecnosc.Find(id);
            db.Nieobecnosc.Remove(nieobecnosc);
            db.SaveChanges();
            return RedirectToAction("Nieobecnosci");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult Zaplata()
        {
            string currentMonth = DateTime.Now.Month.ToString();

            string currentYear = DateTime.Now.Year.ToString();

            if (currentMonth == "1")
            {
                currentMonth = "Styczen";
            }
            else if (currentMonth == "2")
            {
                currentMonth = "Luty";
            }
            else if (currentMonth == "3")
            {
                currentMonth = "Marzec";
            }
            else if (currentMonth == "4")
            {
                currentMonth = "Kwiecien";
            }
            else if (currentMonth == "5")
            {
                currentMonth = "Maj";
            }
            else if (currentMonth == "6")
            {
                currentMonth = "Czerwiec";
            }
            else if (currentMonth == "7")
            {
                currentMonth = "Lipiec";
            }
            else if (currentMonth == "8")
            {
                currentMonth = "Sierpien";
            }
            else if (currentMonth == "9")
            {
                currentMonth = "Wrzesien";
            }
            else if (currentMonth == "10")
            {
                currentMonth = "Pazdziernik";
            }
            else if (currentMonth == "11")
            {
                currentMonth = "Listopad";
            }
            else if (currentMonth == "12")
            {
                currentMonth = "Grudzien";
            }

            var user = User.Identity.GetUserId();

            var dziecko = db.Dziecko.First(u => u.konto_id == user);           

            var miesiac = db.Miesiac.First(u => u.nazwa == currentMonth);

            var rok = db.Rok.First(u => u.rok1 == currentYear);

            var oplata = db.Oplata.Where(x => x.miesiac_id == miesiac.id).Where(x => x.rok_id == rok.id);

            var oneOplata = oplata.FirstOrDefault();

            var transakcja = db.Transakcja.Where(x => x.dziecko_id == dziecko.id).Where(x => x.oplata_id == oneOplata.id).ToList();

            var view = new Transakcja_Oplata()
            {
                Transakcja = transakcja,
                Oplata = oplata
            };

            return View(view);
        }

        public ActionResult PaymentWithPaypal(string Cancel = null)
        {
            //getting the apiContext  
            APIContext apiContext = PaypalConfiguration.GetAPIContext();
            try
            {
                //A resource representing a Payer that funds a payment Payment Method as paypal  
                //Payer Id will be returned when payment proceeds or click to pay  
                string payerId = Request.Params["PayerID"];
                if (string.IsNullOrEmpty(payerId))
                {
                    //this section will be executed first because PayerID doesn't exist  
                    //it is returned by the create function call of the payment class  
                    // Creating a payment  
                    // baseURL is the url on which paypal sendsback the data.  
                    string baseURI = Request.Url.Scheme + "://" + Request.Url.Authority + "/Home/PaymentWithPayPal?";
                    //here we are generating guid for storing the paymentID received in session  
                    //which will be used in the payment execution  
                    var guid = Convert.ToString((new Random()).Next(100000));
                    //CreatePayment function gives us the payment approval url  
                    //on which payer is redirected for paypal account payment  
                    var createdPayment = this.CreatePayment(apiContext, baseURI + "guid=" + guid);
                    //get links returned from paypal in response to Create function call  
                    var links = createdPayment.links.GetEnumerator();
                    string paypalRedirectUrl = null;
                    while (links.MoveNext())
                    {
                        Links lnk = links.Current;
                        if (lnk.rel.ToLower().Trim().Equals("approval_url"))
                        {
                            //saving the payapalredirect URL to which user will be redirected for payment  
                            paypalRedirectUrl = lnk.href;
                        }
                    }
                    // saving the paymentID in the key guid  
                    Session.Add(guid, createdPayment.id);
                    return Redirect(paypalRedirectUrl);
                }
                else
                {
                    // This function exectues after receving all parameters for the payment  
                    var guid = Request.Params["guid"];
                    var executedPayment = ExecutePayment(apiContext, payerId, Session[guid] as string);
                    //If executed payment failed then we will show payment failure message to user  
                    if (executedPayment.state.ToLower() != "approved")
                    {
                        return View("FailureView");
                    }
                }
            }
            catch (Exception ex)
            {
                return View("FailureView");
            }
            

            string currentMonth = DateTime.Now.Month.ToString();

            string currentYear = DateTime.Now.Year.ToString();

            if (currentMonth == "1")
            {
                currentMonth = "Styczen";
            }
            else if (currentMonth == "2")
            {
                currentMonth = "Luty";
            }
            else if (currentMonth == "3")
            {
                currentMonth = "Marzec";
            }
            else if (currentMonth == "4")
            {
                currentMonth = "Kwiecien";
            }
            else if (currentMonth == "5")
            {
                currentMonth = "Maj";
            }
            else if (currentMonth == "6")
            {
                currentMonth = "Czerwiec";
            }
            else if (currentMonth == "7")
            {
                currentMonth = "Lipiec";
            }
            else if (currentMonth == "8")
            {
                currentMonth = "Sierpien";
            }
            else if (currentMonth == "9")
            {
                currentMonth = "Wrzesien";
            }
            else if (currentMonth == "10")
            {
                currentMonth = "Pazdziernik";
            }
            else if (currentMonth == "11")
            {
                currentMonth = "Listopad";
            }
            else if (currentMonth == "12")
            {
                currentMonth = "Grudzien";
            }

            var user = User.Identity.GetUserId();
            var dziecko = db.Dziecko.First(u => u.konto_id == user);
            var miesiac = db.Miesiac.First(u => u.nazwa == currentMonth);
            var rok = db.Rok.First(u => u.rok1 == currentYear);
            var oplata = db.Oplata.Where(x => x.miesiac_id == miesiac.id).Where(x => x.rok_id == rok.id);
            var oneOplata = oplata.FirstOrDefault();



            Transakcja transakcja = new Transakcja();

            transakcja.dziecko_id = dziecko.id;
            transakcja.oplata_id = oneOplata.id; 
            transakcja.kwota = oneOplata.cena;
            transakcja.data_zaplaty = DateTime.Now;

            db.Transakcja.Add(transakcja);
            db.SaveChanges();
            //on successful payment, show success page to user.  
            return View("SuccessView");
        }
        private PayPal.Api.Payment payment;
        private Payment ExecutePayment(APIContext apiContext, string payerId, string paymentId)
        {
            var paymentExecution = new PaymentExecution()
            {
                payer_id = payerId
            };
            this.payment = new Payment()
            {
                id = paymentId
            };
            return this.payment.Execute(apiContext, paymentExecution);
        }
        private Payment CreatePayment(APIContext apiContext, string redirectUrl)
        {
            string currentMonth = DateTime.Now.Month.ToString();

            string currentYear = DateTime.Now.Year.ToString();

            if (currentMonth == "1")
            {
                currentMonth = "Styczen";
            }
            else if (currentMonth == "2")
            {
                currentMonth = "Luty";
            }
            else if (currentMonth == "3")
            {
                currentMonth = "Marzec";
            }
            else if (currentMonth == "4")
            {
                currentMonth = "Kwiecien";
            }
            else if (currentMonth == "5")
            {
                currentMonth = "Maj";
            }
            else if (currentMonth == "6")
            {
                currentMonth = "Czerwiec";
            }
            else if (currentMonth == "7")
            {
                currentMonth = "Lipiec";
            }
            else if (currentMonth == "8")
            {
                currentMonth = "Sierpien";
            }
            else if (currentMonth == "9")
            {
                currentMonth = "Wrzesien";
            }
            else if (currentMonth == "10")
            {
                currentMonth = "Pazdziernik";
            }
            else if (currentMonth == "11")
            {
                currentMonth = "Listopad";
            }
            else if (currentMonth == "12")
            {
                currentMonth = "Grudzien";
            }

            var user = User.Identity.GetUserId();
            var dziecko = db.Dziecko.First(u => u.konto_id == user);
            var miesiac = db.Miesiac.First(u => u.nazwa == currentMonth);
            var rok = db.Rok.First(u => u.rok1 == currentYear);
            var oplata = db.Oplata.Where(x => x.miesiac_id == miesiac.id).Where(x => x.rok_id == rok.id);
            var oneOplata = oplata.FirstOrDefault();

            //create itemlist and add item objects to it  
            var itemList = new ItemList()
            {
                items = new List<Item>()
            };
            //Adding Item Details like name, currency, price etc  
            itemList.items.Add(new Item()
            {
                name = "Stołówka",
                currency = "PLN",
                price = oneOplata.cena.ToString(),
                quantity = "1",
                sku = "sku"
            });
            var payer = new Payer()
            {
                payment_method = "paypal"
            };
            // Configure Redirect Urls here with RedirectUrls object  
            var redirUrls = new RedirectUrls()
            {
                cancel_url = redirectUrl + "&Cancel=true",
                return_url = redirectUrl
            };
            // Adding Tax, shipping and Subtotal details  
            var details = new Details()
            {
                tax = "0",
                shipping = "0",
                subtotal = oneOplata.cena.ToString()
            };
            //Final amount with details  
            var amount = new Amount()
            {
                currency = "PLN",
                total = oneOplata.cena.ToString(), // Total must be equal to sum of tax, shipping and subtotal. 
                details = details
            };
            var transactionList = new List<Transaction>();
            // Adding description about the transaction  
            transactionList.Add(new Transaction()
            {
                description = "Transaction description",
                //invoice_number = "5", //Generate an Invoice No  
                amount = amount,
                item_list = itemList
            });
            this.payment = new Payment()
            {
                intent = "sale",
                payer = payer,
                transactions = transactionList,
                redirect_urls = redirUrls
            };
            // Create a payment using a APIContext  
            return this.payment.Create(apiContext);
        }

    }
}