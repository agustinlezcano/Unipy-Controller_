import socket
import serial
import serial.tools.list_ports
import numpy as np
from spatialmath import SE3
import roboticstoolbox as rtb
'''
DE ARDUINO A PYTHON PASAR EL STRING CON 3 DATOS, DE AHI TRANSFORMAR Y PASAR ESO AL CUBO, LUEGO VER SI ANDA CON VARIOS DATOS
'''

def interpolation(x,lim1,lim2):
    y = lim1 + (lim2-lim1)/(255-0)*(x-0)
    return y
  

def code(string):
    t2 = tuple([round(x,4) if isinstance(x, float) else x for x in i])
    posData = str(t2)
    translation = {44: 47, 40: 58, 41: 46, 32:00}
    posData = posData.translate (translation)
    return posData


puerto = serial.tools.list_ports.comports()

ports = list()
for port in sorted(puerto):
        print("{}".format(port))
        com = str(port).split(" ")[0]
        ports.append(com)

indice_puerto =input("Número de Puerto: ")
for i in ports:
    if i == "COM"+indice_puerto:
                arduino = serial.Serial(i)

print("OK")     #Hasta acá anda bien

#Creacion de Robot
#puma = rtb.models.DH.Puma560()
#La medida del SE3 es en metros
t = np.arange(0, 2, 0.010)
#T0 = SE3(0.6, -0.5, 0.0)

textoViejo = "999999999"
i=0 #BORRAR

#host, port = "127.0.0.1", 25001
#data = "1,2,3"
IP = input("Ingrese la IP del celular: ")
PORT = 25001

# SOCK_STREAM: TCP socket
sock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
server_address = (IP, PORT)
print('Conectándose a {} puerto {}'.format(*server_address))


#Pasar los parámetros de client_Python y darle valores a un display en Unity
while True:
    texto = arduino.readline()
    texto = texto.decode("ascii")

    if len(texto)>0 and (texto[0]==":"):
        if (texto != textoViejo):
            #if (texto[1:4]) # NO comparar el modulo de cada parte(no tomaria -127 y 127 ))
            print("texto: ",texto)
            print("rep: ",i)
            i=i+1  
            #CINEMATICA INVERSA
            '''
            #Convierto la entrada en espacio 3 (decodificar primero)
            y1 = interpolation(int(texto[1:4]),0,0.864)
            y2 = interpolation(int(texto[4:7]),0,0.864)
            y3 = interpolation(int(texto[7:10]),0,0.550)          
            print("{} {} {}".format(y1,y2,y3))
            T1 = SE3(y1,y2,y3)
            
            #Realizar trayectoria en coordenadas cartesianas (ctraj)
            Ts = rtb.tools.trajectory.ctraj(T0, T1, len(t)) #Evaluar Jtraj
            sol = puma.ikine_LM(Ts)       # named tuple of arrays
            '''

            ##No cinematica inversa
            textoViejo = texto
            #T0 = T1    #VOLVER A PONER
            #Probar si no es mejor que salga del IF (uno para atras)
            try:
                sock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)    #si no funciona probar en el FOR
                # Connect to the server and send the data
                #sock.connect((host, port))
                cont = 0
                sock.connect(server_address)
                #CINEMATICA INVERSA
                '''
                for i in sol.q:
                    cont = cont+1
                    posData = code(i)
                    print(posData)
                    #aca crear objeto socket
                    sock.send(posData.encode("utf-8"))   #sock.sendall(posData.encode("utf-8"))
                    #sendall funciona si los datos cambian a una menor velocidad (Para el stream dado por arduino funciona).
                    #send no funciona (en teria espera que los datos se envien por completo.
                    #SOLUCIONES: Usar algún delay por cada dato y sino usar el potenciometro y un boton para mandar angulos directamente
                    time.sleep(0.017)   #0.017
                    #response = sock.recv(1024).decode("utf-8")
                    #print (response)
                '''
                sock.sendall(texto.encode("utf-8"))        #para enviar lo de arduino         
                #response = sock.recv(1024).decode("utf-8")
                #print (response)

            finally:
                sock.close()
                sock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
