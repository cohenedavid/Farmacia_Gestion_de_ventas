""" 
    STRINGS
    REBANADAS

"""


from xml.etree.ElementTree import tostring


word = "Python"

rebanada1 = word[0:6]   # desde el caracter 0 (incluido) hasta el 6 (excluido)
rebanada2 = word[0:]    # desde el caracter 0 (incluido) hasta el ultimo de la cadena

rebanada3 = word[0:3]   # desde el caracter 0 (incluido) hasta el 3 (excluido)

                        # Para índices NO negativos, la longitud de la rebanada es la 
                        # diferencia de los índices, si ambos están dentro de los límites
                        # en una rebanada siempre el primer valor se incluye y el segundo se excluye
                        # cadena[incluido: excluido]
print(rebanada1)
print(rebanada3)

print(word + ' Tutorial') # imprimir variable concatenado a un literal


"""
 +---+---+---+---+---+---+
 | P | y | t | h | o | n |
 +---+---+---+---+---+---+
 0   1   2   3   4   5   6
-6  -5  -4  -3  -2  -1
"""

# os índices de rebanadas fuera de rango se manejan satisfactoriamente cuando se usan para rebanar
print(word[4:86])

# los índices de una cadena son inmutables, por eso no se pueden reasignar nuevos caracteres
"""word[0] = "R" --> lanza el siguiente error: """
# TypeError: 'str' object does not support item assignment

# Para generar una cadena nueva basada en otra creada anteriormente, sería por ejemplo:

word2 = "R" + word[1:]
print(word2)

# longitud de una cadena con la función incorporada len()
# devuelve un entero positivo
print(len(word))