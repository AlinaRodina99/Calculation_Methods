import numpy as np
from numpy.linalg import norm, cond, solve, inv
from scipy.linalg import hilbert

def simple_iterations(matrix, b, x_0, epsilon):
    c = np.zeros((matrix.shape[0], matrix.shape[1]))
    d = np.zeros(b.shape[0])
    for i in range(matrix.shape[0]):
        d = b[i] / matrix[i][i]
        for j in range(matrix.shape[1]):
            if i != j:
                c[i][j] = -matrix[i][j] / matrix[i][i]
    x_1 = c @ x_0 + d
    iterations = 1
    while(norm(x_1 - x_0) >= epsilon and iterations <= 200):
        x_0 = x_1
        x_1 = c @ x_0 + d 
        iterations += 1
    return x_1, iterations

def seidel(matrix, b, x_0, epsilon):
    n = len(matrix)  
    converge = False
    x = x_0
    iterations = 0
    while (not converge and iterations <= 200):
        x_new = np.copy(x)
        for i in range(n):
            s1 = sum(matrix[i][j] * x_new[j] for j in range(i))
            s2 = sum(matrix[i][j] * x[j] for j in range(i + 1, n))
            x_new[i] = (b[i] - s1 - s2) / matrix[i][i]

        converge = norm(x_new[i] - x[i]) <= epsilon
        x = x_new
        iterations += 1
    return x, iterations

def print_matrix(matrix):
    for i in range(matrix.shape[0]):
        for j in range(matrix.shape[1]):
            print(matrix[i][j], end = ' ')
        print('\n')

def print_report_simple_iterations(matrix, b):
    print("Метод простых итераций")
    for i in range(3, 12, 4):
        print("Текущий epsilon: ", 10 ** (-i))
        print("Погрешность решения: ", norm(solve(matrix, b) - simple_iterations(matrix, b, b, 10**(-i))[0]))
        print("Количество итераций: ", simple_iterations(matrix, b, b, 10**(-i))[1])
    print('\n')

def print_report_seidel(matrix, b):
    print("Метод Зейделя")
    for i in range(3, 12, 4):
        print("Текущий epsilon: ", 10 ** (-i))
        print("Погрешность решения: ", norm(solve(matrix, b) - seidel(matrix, b, b, 10**(-i))[0]))
        print("Количество итераций: ", seidel(matrix, b, b, 10**(-i))[1])
    print('\n')

def main():
    print("Матрица из методички Пакулиной:")
    matrix_1 = np.array([[-401.64, 200.12], [21200.72,  -601.76]])
    print_matrix(matrix_1)
    b_1 = np.array([[-202.92], [40596.16]])
    print_report_simple_iterations(matrix_1, b_1)
    print_report_seidel(matrix_1, b_1)
    print("Матрица из методички Пакулиной")
    matrix_2 = np.array([[-400.94, 200.02], [1200.12, -600.96]])
    print_matrix(matrix_2)
    b_2 = np.array([[-201.82], [597.36]])
    print_report_simple_iterations(matrix_2, b_2)
    print_report_seidel(matrix_2, b_2)
    print("Матрица Гильберта 3-го порядка")
    print_report_simple_iterations(hilbert(3), np.array([[11/6], [13/12], [47/60]]))
    print_report_seidel(hilbert(3), np.array([[11/6], [13/12], [47/60]]))
    print("Матрица Гильберта 4-го порядка")
    print_report_simple_iterations(hilbert(4), np.array([[25/12], [77/60], [19/20], [319/420]]))
    print_report_seidel(hilbert(3), np.array([[11/6], [13/12], [47/60]]))
    print("Матрица Гильберта 5-го порядка")
    print_report_simple_iterations(hilbert(5), np.array([[137/60], [29/20],[153/140],[743/840],[1879/2520]]))
    print_report_seidel(hilbert(3), np.array([[11/6], [13/12], [47/60]]))
    print("Матрица Гильберта 6-го порядка")
    print_report_simple_iterations(hilbert(6), np.array([[49/20], [223/140], [341/280], [2509/2520], [2131/2520], [20417/27720]]))
    print_report_seidel(hilbert(3), np.array([[11/6], [13/12], [47/60]]))
    
main()