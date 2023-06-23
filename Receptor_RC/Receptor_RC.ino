#include <RF24_config.h>
#include <nRF24L01.h>
#include <RF24.h>
#include <SPI.h>
#include <Wire.h> 
#include <LiquidCrystal_I2C.h>

//SI CAMBIO EL TRANSMISOR (DATOS) CAMBIAR EL NUMERO DE DATO EN EL ARRAY
LiquidCrystal_I2C lcd(0x27,16,2);  //0x3F

int cont;
byte addr[5] ={0,0,0,0,1};
RF24 radio(9, 8); //CE, CSN
byte data[14];
int dataSend[6];
int oldDataSend[6];    //Contenedor para comparar datos con los anteriores
int newDataSend[6];
bool flag = true;
long i2cData[6];
int yMin[] = {-160,-45,-225,-110,-100,-266};    //Ángulos mínimos por articulación
int yMax[] = {160,225,45,170,100,266};          //Ángulos máximos por articulación
bool flagConexion = false;

//Funciones
void imprime_ceros(int x);
int anti_ruido(int x);
void menu_inicial();
//void convertir_angulo(int x);   //Esto se puede hacer desde Unity
int interpolacion_lineal(int x, int i);   //No hace falta las x porque son 0 y 255

void setup(){
  cont=0;
  Serial.begin(9600); 
  radio.begin();
  radio.setChannel(110);
  radio.openReadingPipe(1, addr);
  radio.setPALevel(RF24_PA_MIN);
  radio.startListening();
  //int oldDataSend[6] = {0};
  // Inicializar el LCD
  lcd.init();
  lcd.clear();
  //Encender la luz de fondo.
  lcd.backlight();
  menu_inicial(); 
}
 //PROBLEMA: cuando sigo con el botón 12 y el contador es 6 no vuelve a 0 sino que muestra 7 antes de borrarse
 //PROBLEMA: se repite la ultima instruccion y se muestra el stream desde 7, lo toma como 1 y repite el resto de las instrucciones
 //PROBLEMA: aparecen 0 de "imprime_ceros". SOLUCIONAR EL TEMA DEL RUIDO AL APRETAR EL BOTÓN
 //TAREA: i llega hasta 14 con el codigo viejo, sino hasta 4     
 //SOLUCION: El error de los 00 se soluciona al final del loop, donde tengo el lcd.print y el delay
void loop() {
 if ( radio.available() ){    
     if (flagConexion == false){
      flagConexion=true; 
     }
     radio.read(&data, sizeof(data)); //MUY IMPORTANTE EL AMPERSANT
     //data[9] es para activar la comunicación, data[13] para enviar mensaje, data[12] para ir pasando de articulación
     if (data[9] == 1){
      if (flag){
        lcd.setCursor(0,1);
        lcd.print("Conectado");   //ERROR: sobreescribe los datos que tengo de articulaciones  
        flag=false;
      }
      
      if (data[12] == 0){
        if (cont>=6){
          cont=0;
          }
        dataSend[cont] = data[5];
        
        //DISPLAY LCD
        lcd.setCursor(0,1);
        lcd.print("Art. ");
        lcd.print(cont+1);
        lcd.print(": ");
        //Aca cambie olddatasend por new
        oldDataSend[cont] = interpolacion_lineal(dataSend[cont], cont); 
        imprime_ceros(oldDataSend[cont]);
        lcd.print(oldDataSend[cont]);
        lcd.print(" ");
        cont++;
      }
      if (data[13] == 0){
        //if (oldDataSend[1] == 999){        
          if (cont<5){
            for (int j=cont+1;j<6;j++){
              dataSend[j]=data[5]; 
            }
          }
          lcd.clear();
          cont = 0;
          Serial.print(":");
          for (int i = 0; i < 6; i++){
            if (i==5){
              oldDataSend[i] = interpolacion_lineal(dataSend[i], i);
              imprime_ceros(oldDataSend[i]);
              Serial.print(oldDataSend[i]);
              Serial.print(".");
            }
            else{ 
              oldDataSend[i] = interpolacion_lineal(dataSend[i], i);
              imprime_ceros(oldDataSend[i]);
              Serial.print(oldDataSend[i]);
              Serial.print("/");
            }
          }
        }
        //limite dentro del envio de data[13]
      }
     
     else {
      lcd.setCursor(0,1);
      lcd.print("Desconectado");
      }
     Serial.println(" ");     
     //lcd.clear();
     lcd.setCursor(0,0);
     lcd.print("Actual: ");
     //Acá cambié oldDataSend con newDataSend ------------------------
     oldDataSend[5] = interpolacion_lineal(data[5], cont);
     //imprime_ceros(oldDataSend[5]);
     lcd.print(oldDataSend[5]);
     lcd.print(" ");
     }
     else{
      if (flagConexion == true){
        lcd.clear();
        flagConexion = false;  
      }
      }
     delay(250);
 }


//Funcion para que se muestren los valores todos con 3 cifras
void imprime_ceros(int x){
  if(x>=0 && x<10){
    Serial.print("00");
    lcd.print("00");
  }
  if(x>=10 && x<100){
    Serial.print("0");
    lcd.print("0");
  }
}
//Funcion para evitar ruido en el valor medio (127)
int anti_ruido(int x){
  if (x==128 || x==126){
    x=127;
    }
  return x;
}

//Funcion de mensaje inicial
void menu_inicial(){
  lcd.setCursor(5,0);
  lcd.print("UniPy");
  lcd.setCursor(2,1);
  lcd.print("Controller");
  delay(100); 
  for (int i=0;i<16;i++){
    lcd.scrollDisplayLeft();
    delay(150); 
  }
  lcd.clear();
}

//Regresion lineal
//VER DE PASAR DOS ARRAYS CON MINIMOS Y MAXIMOS Y HACER TODO EN CONVERTIR_ANGULO
//Tengo arrays con variables globales, hacer IF con la posición del dataSend
//Primer parametro es valor de datasend, el segundo es la posicion
int interpolacion_lineal(int x, int i){
  int y;
  y = int(map(x, 0, 255, yMin[i],yMax[i]));
  if (y>=0){
    lcd.print("+");
  }
  return y;
}



//Función de Stay alive
