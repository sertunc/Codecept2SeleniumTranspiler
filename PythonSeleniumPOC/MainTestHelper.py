#General Imports
from pathlib import Path
from logging import error
import os
import sys
import time
import os.path
import random

#Selenium Imports (Chrome)
from selenium import webdriver
from selenium.webdriver.chrome.options import Options
from selenium.webdriver.support.wait import WebDriverWait 
from selenium.webdriver.common.by import By
from selenium.webdriver.support import expected_conditions as EC
from selenium.webdriver.common.action_chains import ActionChains

# OUTPUT_FOLDER = "D:\\resimler\\"
# OUTPUT_FOLDER = "/scripts/";
OUTPUT_FOLDER = "C:/yedek_sertunc_selen/source/repos/Codecept2SeleniumTranspiler/PythonSeleniumPOC/scripts/"

class MainTestHelper:
    def __init__(self):
        chrome_options = Options()
        #chrome_options.add_argument("--headless")
        #chrome_options.add_argument("--no-sandbox")
        #chrome_options.add_argument("--disable-gpu")
        self.chrome_options = chrome_options
        self.order = 1

        self.driver = webdriver.Chrome(options=chrome_options)

    def TesteBasla(self):
        try:
            self.TestTanimi()
            self.driver.close()
            print("Test Completed Successfully")
    
        except Exception as Error:
            self.BeforeAction("HATA - {hata}".format(hata = Error))
        
    #Below method will be overriden
    def TestTanimi(self):
        pass

    #public methods

    def Git(self, url: str):
        self.BeforeAction("{url} adresine git".format(url = url))
        self.driver.get(url)

    def Tikla(self, path: str, aciklama = ""):
        if len(aciklama) == 0:
            self.BeforeAction("Düğmeye bas (Path={path}))".format(path = path))
        else:
            self.BeforeAction("{aciklama} düğmesine bas (Path={path}))".format(aciklama = aciklama, path = path))
        self.findSingleControl(path, aciklama).click()

    def Yaz(self, path: str, content: str, cleanFirst = False, aciklama = ""):
        if len(aciklama) == 0:
            self.BeforeAction("Alana yazı yaz => {content} (Path={path}))".format(content = content, path = path))
        else:
            self.BeforeAction("{aciklama} alanına yazı yaz => {content} (Path={path}))".format(aciklama = aciklama, content = content, path = path))

        found = self.findSingleControl(path, aciklama)
        if cleanFirst == True:
            found.clear()
        found.send_keys(content)

    def Bekle(self, sure: int):
        self.BeforeAction("{sure} saniye bekle".format(sure = sure))

        try:
            return WebDriverWait(self.driver, timeout=sure, poll_frequency=1).until(lambda x: False);
        except:
            pass

    def BekleVeGetir(self, sure: int, path: str, aciklama = ""):
        if len(aciklama) == 0:
            self.BeforeAction("Eleman getir (en fazla {sure} saniye) (Path={path})".format(sure = sure, path = path))
        else:
            self.BeforeAction("{aciklama} elemanını getir (en fazla {sure} saniye) (Path={path})".format(sure = sure, path = path, aciklama = aciklama))
        
        return WebDriverWait(self.driver, sure).until(lambda x: self.findElementByDriver(x, path))
    
    def SayfadaVarMi(self, beklenenMetin: str, path: str = "", aciklama = ""):        
        if len(aciklama) == 0:
            self.BeforeAction("Sayfada '{beklenenMetin}' metni aranıyor.".format(beklenenMetin=beklenenMetin))
        else:
            self.BeforeAction("{aciklama}: Sayfada '{beklenenMetin}' metni aranıyor.".format(aciklama=aciklama, beklenenMetin=beklenenMetin))
        
        try:
            if path:
                # Belirli bir kontrol içinde metni arar
                kontrol = self.findSingleControl(path, aciklama)
                gercekMetin = kontrol.text.strip()
                if beklenenMetin not in gercekMetin:
                    raise AssertionError(f"Beklenen metin '{path}' kontrol içinde bulunamadı. Varolan metin: '{gercekMetin}'")
            else:
                # Tüm sayfada metni arar
                body = self.driver.find_element(By.TAG_NAME, "body")
                if beklenenMetin not in body.text:
                    raise AssertionError(f"Beklenen metin '{beklenenMetin}' sayfada bulunamadı.")
            
            self.BeforeAction("Sayfada '{beklenenMetin}' metni bulundu.".format(beklenenMetin=beklenenMetin))
        except Exception as e:
            raise AssertionError(f"'{beklenenMetin}' metni kontrol edilirken hata oluştu: {str(e)}")
        
    def GizlenmesiniBekle(self, path: str, sure: int = 10, aciklama = ""):
        if len(aciklama) == 0:
            self.BeforeAction("Kontrolün kaybolması bekleniyor (Path={path}, Süre={sure} saniye)".format(path=path, sure=sure))
        else:
            self.BeforeAction("{aciklama}: Kontrolün kaybolması bekleniyor (Path={path}, Süre={sure} saniye)".format(aciklama=aciklama, path=path, sure=sure))
        
        try:
            WebDriverWait(self.driver, timeout=sure).until(EC.invisibility_of_element_located(self.findElementByDriver(self.driver, path)))
            self.BeforeAction("Kontrol kayboldu: {path}".format(path=path))
        except Exception as e:
            raise AssertionError(f"Kontrol belirtilen süre içinde kaybolmadı: {path}. {str(e)}")
        
    def GorunmesiniBekle(self, path: str, sure: int = 10, aciklama = ""):
        if len(aciklama) == 0:
            self.BeforeAction("Kontrolün görünür hale gelmesi bekleniyor (Path={path}, Süre={sure} saniye)".format(path=path, sure=sure))
        else:
            self.BeforeAction("{aciklama}: Kontrolün görünür hale gelmesi bekleniyor (Path={path}, Süre={sure} saniye)".format(aciklama=aciklama, path=path, sure=sure))
        
        try:
            # Locator oluştur
            if path.startswith("#"):
                locator = (By.CSS_SELECTOR, path)
            elif path.startswith("."):
                locator = (By.CSS_SELECTOR, path)
            elif path.startswith("//") or path.startswith("/"):
                locator = (By.XPATH, path)
            else:
                raise ValueError(f"Geçersiz path formatı: {path}")
            
            WebDriverWait(self.driver, timeout=sure).until(EC.visibility_of_element_located(locator))
            self.BeforeAction("Kontrol görünür hale geldi: {path}".format(path=path))
        except Exception as e:
            raise AssertionError(f"Kontrol belirtilen süre içinde görünür hale gelmedi: {path}. {str(e)}")
        
    def AcilirKutudaSec(self, path: str, value: str, aciklama: str = ""):
        if len(aciklama) == 0:
            self.BeforeAction("Açılır menüden seçenek seçiliyor (Path={path}, Value={value})".format(path=path, value=value))
        else:
            self.BeforeAction("{aciklama}: Açılır menüden seçenek seçiliyor (Path={path}, Value={value})".format(aciklama=aciklama, path=path, value=value))
        
        try:
            dropdown = self.findSingleControl(path, aciklama)
            for option in dropdown.find_elements(By.TAG_NAME, "option"):
                if option.get_attribute("value") == value:
                    option.click()
                    self.BeforeAction("Seçenek seçildi: {value}".format(value=value))
                    return
            raise AssertionError(f"Açılır menüde '{value}' değeri bulunamadı: {path}")
        except Exception as e:
            raise AssertionError(f"Açılır menüden seçenek seçilirken hata oluştu: {path}. Hata: {str(e)}")
        
    def ImleciGotur(self, path: str, xoffset: int, yoffset: int, aciklama: str = ""):
        if len(aciklama) == 0:
            self.BeforeAction(f"İmleç {path} elementinin üzerine götürülüyor.")
        else:
            self.BeforeAction(f"{aciklama}: İmleç {path} elementinin üzerine götürülüyor.")

        try:
            element = self.findSingleControl(path, aciklama)
            actions = ActionChains(self.driver)
            actions.move_to_element_with_offset(element, xoffset, yoffset).perform()
            self.BeforeAction(f"İmleç {path} elementinin üzerine götürüldü.")
        except Exception as e:
            raise AssertionError(f"İmleç {path} elementinin üzerine götürülürken hata oluştu: {str(e)}")

    #helper methods

    def EkranGoruntusu(self):
        try:
            self.driver.get_screenshot_as_file("{folderPrefix}{order}.png".format(folderPrefix = OUTPUT_FOLDER, order = self.order));
    
        except Exception as Error:
            print(Error)

    def BeforeAction(self, log: str):
        myLog = "--- {order}. {log}".format(order = self.order, log = log)
        with open("{folderPrefix}log.txt".format(folderPrefix = OUTPUT_FOLDER), "a") as myfile:
            myfile.write(myLog + "\n")
        print(myLog)
        self.EkranGoruntusu()
        self.order = self.order + 1


    def findSingleControl(self, path: str, aciklama: str):
       return self.findSingleControlByDriver(self.driver, path, aciklama);

    def findSingleControlByDriver(self, givenDriver: webdriver.Chrome, path: str, aciklama: str):
        if path.startswith("#"):
            byType = By.ID
            pathToUse = path[1::]
        elif path.startswith("."):
            byType = By.CSS_SELECTOR
            pathToUse = path[1::]
        elif path.startswith("//") or path.startswith("/"):
            byType = By.XPATH
            pathToUse = path
        else:
            byType = By.XPATH
            pathToUse =  f"//*[text()='{path}']" 

#        "//*[@id=\"root\"]/div/div[3]/div[3]/button"
        controls = givenDriver.find_elements(byType, pathToUse)
        if len(controls) == 0:
            if aciklama == "":
                raise BaseException(f"ifadeye sahip kontrol bulunamamıştır: {path}")
            else:
                raise BaseException(f"{aciklama} bulmak için ifadeye sahip kontrol bulunamamıştır: {path}")

        if len(controls) > 1:
            if aciklama == "":
                raise BaseException(f"ifadeye sahip birden fazla kontrol bulunmuştur. İfade: {path}")
            else:
                raise BaseException(f"{aciklama} bulmak için ifadeye sahip birden fazla kontrol bulunmuştur. İfade: {path}")

        return controls[0]

    def findElementByDriver(self, givenDriver: webdriver.Chrome, path: str):
        if path.startswith("#"):
            byType = By.ID
            pathToUse = path[1::]
        elif path.startswith("."):
            byType = By.CSS_SELECTOR
            pathToUse = path[1::]
        elif path.startswith("//"):
            byType = By.XPATH
            pathToUse = path
        else:
            byType = By.LINK_TEXT
            pathToUse = path

        return givenDriver.find_element(byType, pathToUse)