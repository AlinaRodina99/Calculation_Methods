import numpy as np
from numpy.linalg import solve, norm,cond
from scipy.linalg import hilbert
import copy

def qr_decomposition(matrix):
    n = matrix.shape[0]
    q = np.identity(n)
    r = copy.copy(matrix)
    for i in range(n):
        for j in range(i+1,n):
            c = r[i,i]/(r[i,i]**2 + (r[j,i]**2))**0.5
            s = r[j,i]/(r[i,i]**2 + (r[j,i]**2))**0.5
            r[i,:], r[j,:] = c*r[i,:] + s*r[j,:], -s*r[i,:] + c*r[j,:]
            q[:,i], q[:,j] = c*q[:,i] + s*q[:,j], -s*q[:,i] + c*q[:,j]
    return (q,r)

def qr_solution(matrix, b):
    q = qr_decomposition(matrix)[0]
    r = qr_decomposition(matrix)[1]
    y = solve(q, b)
    x = solve(r, y)
    return x    

def regularization(matrix, b):
    x = qr_solution(matrix, b)
    E = np.identity(matrix.shape[0])
    for i in range(1, 13, 3):
        matrix_i = matrix + 10 ** (-i) * E
        x_i = qr_solution(matrix_i, b)
        print("Текущее alpha: ", 10 ** (-i))
        print("Число обусловленности для данного alpha: ", cond(matrix_i))
        print("Погрешность решения: ", norm(x - x_i))

def main():
    print("Посмотрим на погрешность решения с помощью qr-разложения:")
    print("Для матрицы Гильберта 3 порядка:")
    print("Погрешность")
    print(norm(solve(hilbert(3), np.array([[11/6], [13/12], [47/60]])) - qr_solution(hilbert(3), np.array([[11/6], [13/12], [47/60]]))))
    print('\n')
    print("Для матрицы Гильберта 4 порядка:")
    print("Погрешность")
    print(norm(solve(hilbert(4), np.array([[25/12], [77/60], [19/20], [319/420]])) - qr_solution(hilbert(4), np.array([[25/12], [77/60], [19/20], [319/420]]))))
    print('\n')
    print("Метод регуляризации")
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