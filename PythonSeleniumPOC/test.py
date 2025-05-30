from MainTestHelper import MainTestHelper
from Random import Random

class SayfadaVarMiTest(MainTestHelper):
    def TestTanimi(self):
        self.Git("http://localhost:5173/")
        self.Bekle(2)
        self.Yaz("#username", "sertunc", True)
        self.Yaz("#password", "123", True)
        self.Tikla("#btnLogin")
        self.Bekle(2)        
        #self.SayfadaVarMi("Giriş Başarılı")
        self.SayfadaVarMi("Giriş Başarılı","#lblResult")

class GizlenmesiniBekleTest(MainTestHelper):
    def TestTanimi(self):
        self.Git("http://localhost:5173/")
        self.Bekle(2)
        self.Tikla("#btnGizlenmesiniBekle")
        self.GizlenmesiniBekle("#divGizlenmesiniBekle", 5)        

class GorunmesiniBekleTest(MainTestHelper):
    def TestTanimi(self):
        self.Git("http://localhost:5173/")
        self.Bekle(2)
        self.Tikla("#btnGorunmesiniBekle")        
        self.GorunmesiniBekle("#divGorunmesiniBekle", 5)

class CheckOptionTest(MainTestHelper):
    def TestTanimi(self):
        self.Git("http://localhost:5173/")
        self.Bekle(2)
        self.Tikla("#checkbox1", "Kullanım koşulları checkbox'ını işaretle")
        self.Bekle(5)

class AcilirKutudaSecTest(MainTestHelper):
    def TestTanimi(self):
        self.Git("http://localhost:5173/")
        self.Bekle(2)
        self.AcilirKutudaSec("#dropdown", "option2", "Seçenek 2'yi seç")
        self.Bekle(2)
        self.SayfadaVarMi("Seçilen seçenek: option2", "#selectedOptionLabel", "Seçimin doğru olduğunu kontrol et")
        self.Bekle(5)

class ImleciGoturTest(MainTestHelper):
    def TestTanimi(self):
        self.Git("http://localhost:5173/")
        self.Bekle(2)
        self.ImleciGotur("#hoverButton", 0, 0, "Hover butonuna imleci götür")
        self.Bekle(2)
        

if __name__ == "__main__":
    #SayfadaVarMiTest().TesteBasla()
    #GizlenmesiniBekleTest().TesteBasla()
    #GorunmesiniBekleTest().TesteBasla()
    #CheckOptionTest().TesteBasla()
    #AcilirKutudaSecTest().TesteBasla()
    ImleciGoturTest().TesteBasla()
    
    # random = Random()

    # print(random.name.firstName())
    # print(random.name.lastName())
    # print(random.name.findName())
