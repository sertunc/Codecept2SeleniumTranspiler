from MainTestHelper import MainTestHelper
from Random import Random # from Random(bütük harf yazılmasına dikkat edilmeli aksi halde python random sınıfın algılıyor.

class SayfadaOlmaliTest(MainTestHelper):
    def TestTanimi(self):
        self.Git("http://localhost:5173/")
        self.Bekle(1)
        self.Tikla("#sayfada-olmali-test")
        self.Bekle(1)
        
        self.Yaz("#username", "sertunc", True)
        self.Yaz("#password", "123", True)
        self.Tikla("#btnLogin")
        self.Bekle(2)        
        #self.SayfadaOlmali("Giriş Başarılı")
        self.SayfadaOlmali("Giriş Başarılı", "#lblResult")

class GizlenmesiniBekleTest(MainTestHelper):
    def TestTanimi(self):
        self.Git("http://localhost:5173/")
        self.Bekle(1)
        self.Tikla("#gizlenmesini-bekle-test")
        self.Bekle(1)

        self.Tikla("#btnGizlenmesiniBekle")
        self.GizlenmesiniBekle("#divGizlenmesiniBekle", 5)        

class GorunmesiniBekleTest(MainTestHelper):
    def TestTanimi(self):
        self.Git("http://localhost:5173/")
        self.Bekle(1)
        self.Tikla("#gorunmesini-bekle-test")
        self.Bekle(1)

        self.Tikla("#btnGorunmesiniBekle")        
        self.GorunmesiniBekle("#divGorunmesiniBekle", 5)

class CheckOptionTest(MainTestHelper):
    def TestTanimi(self):
        self.Git("http://localhost:5173/")
        self.Bekle(1)
        self.Tikla("#check-option-test")
        self.Bekle(1)

        self.Tikla("#checkbox1", "Kullanım koşulları checkbox'ını işaretle")
        self.Bekle(1)

class AcilirKutudaSecTest(MainTestHelper):
    def TestTanimi(self):
        self.Git("http://localhost:5173/")
        self.Bekle(1)
        self.Tikla("#acilir-kutuda-sec-test")
        self.Bekle(1)

        self.AcilirKutudaSec("#dropdown", "option2", "Seçenek 2'yi seç")
        self.Bekle(1)
        self.SayfadaOlmali("Seçilen seçenek: option2", "#selectedOptionLabel", "Seçimin doğru olduğunu kontrol et")
        self.Bekle(1)

class ImleciUzerineGetirTest(MainTestHelper):
    def TestTanimi(self):
        self.Git("http://localhost:5173/")
        self.Bekle(1)
        self.Tikla("#imleci-uzerine-getir-test")
        self.Bekle(1)

        self.ImleciGotur("#hoverButton", 0, 0, "Hover butonuna imleci götür")
        self.Bekle(1)
        

if __name__ == "__main__":
    #SayfadaOlmaliTest().TesteBasla()
    #GizlenmesiniBekleTest().TesteBasla()
    #GorunmesiniBekleTest().TesteBasla()
    #CheckOptionTest().TesteBasla()
    #AcilirKutudaSecTest().TesteBasla()
    #ImleciUzerineGetirTest().TesteBasla()
    
    random = Random()

    #print(random.name.firstName())
    #print(random.name.lastName())
    #print(random.name.findName())
    #print(random.name.jobTitle())
    #print(random.company.companyName())
    #print(random.phone.phoneNumber())
    #print(random.internet.email())
    #print(random.internet.ip())
