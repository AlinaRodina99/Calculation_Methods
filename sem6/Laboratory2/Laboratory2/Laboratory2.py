from numpy import linalg as LA
import math
import numpy as np
from scipy.linalg import hilbert

def lu_decomposition(matrix):
    l = np.identity(matrix.shape[0])
    u = np.zeros((matrix.shape[0], matrix.shape[0]))
    for i in range(matrix.shape[0]):
        for j in range(matrix.shape[1]):
            if i <= j:
                u[i][j] = matrix[i][j] - sum(l[i][k] * u[k][j] for k in range(i))
            else:
                l[i][j] = (matrix[i][j] - sum(l[i][k] * u[k][j] for k in range(j))) / u[j][j]
    return l, u

def lu_solve(matrix, b):
    l = lu_decomposition(matrix)[0]
    u = lu_decomposition(matrix)[1]
    y = LA.solve(l, b)
    x = LA.solve(u, y)
    return x

def regularization(matrix, b):
    x = lu_solve(matrix, b)
    E = np.identity(matrix.shape[0])
    for i in range(1, 13, 3):
        matrix_i = matrix + 10 ** (-i) * E
        x_i = lu_solve(matrix_i, b)
        print("Текущее alpha: ", 10 ** (-i))
        print("Число обусловленности для данного alpha: ", LA.cond(matrix_i))
        print("Погрешность решения: ", LA.norm(x - x_i))
    

def main():
    print("Проверим, что решение с помощью LU-разложение не сильно отличается от обычного решения:")
    matrix = np.array([[10, -7, 0], [-3, 6, 2], [5, -1, 5]])
    b = np.array([[2], [27], [41]])
    print(LA.norm(LA.solve(matrix, b) - lu_solve(matrix, b)))
    print("Для матрицы Гильберта 3 порядка:")
    regularization(hilbert(3), np.array([[11/6], [13/12], [47/60]]))
    print('\n')
    print("Для матрицы Гильберта 4 порядка:")
    regularization(hilbert(4), np.array([[25/12], [77/60], [19/20], [319/420]]))
    print('\n')
    print("Для матрицы Гильберта 5 порядка:")
    regularization(hilbert(5), np.array([[137/60], [29/20],[153/140],[743/840],[1879/2520]]))
    print('\n')
    print("Для матрицы Гильберта 6 порядка:")
    regularization(hilbert(6), np.array([[49/20], [223/140], [341/280], [2509/2520], [2131/2520], [20417/27720]]))

main()