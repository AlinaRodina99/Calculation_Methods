from numpy import linalg as LA
import math
import numpy as np

def spectrum_cond(matrix):
    inverse_matrix = LA.inv(matrix)
    return LA.norm(matrix) * LA.norm(inverse_matrix)

def volume_cond(matrix):
    product = 1
    for i in range(matrix.shape[0]):
        sum_in_row = 0
        for j in range(matrix.shape[1]):
            sum_in_row += matrix[i][j] ** 2
        product *= math.sqrt(sum_in_row)
    return product / abs(LA.det(matrix))

def angle_cond(matrix):
    list = []
    C = LA.inv(matrix)
    for i in range(matrix.shape[0]):
        list.append(LA.norm(matrix[i, :]) * LA.norm(C[:, i]))
    return max(list)

def print_matrix(matrix):
    for i in range(matrix.shape[0]):
        for j in range(matrix.shape[1]):
            print(matrix[i][j], end = ' ')
        print('\n')

def solution(matrix, b):
    return LA.solve(matrix, b)
    return b

def print_b(b):
    for i in range(b.shape[0]):
        for j in range(b.shape[1]):
            print(b[i][j], end = ' ')
        print('\n')

def hilbert_matrix(n):
    matrix = np.zeros((n, n))
    for i in range(1, n + 1):
        for j in range(1, n + 1):
            matrix[i - 1][j - 1] = 1 / (i + j - 1)
    return matrix

def print_solution(matrix, b):
    print('Матрица:')
    print_matrix(matrix)
    print('Вектор свободных членов')
    print_b(b)
    print('Спектральный критерий обусловленности:')
    print(spectrum_cond(matrix))
    print('\n')
    print('Объемный критерий обусловленности:')
    print(volume_cond(matrix))
    print('\n')
    print("Угловой критерий обусловленности:")
    print(angle_cond(matrix))
    print('\n')
    print("Поварьируем матрицу и посмотрим, насколько изменилось решение:")
    print("Точное решение:")
    x = solution(matrix, b)
    print(x)
    print("Приблизительное решение:")
    print(solution(matrix - 10 ** (-2), b - 10 ** (-2)))
    print("Разница между точным и приблизительным решением, измененным на 10^-2:")
    x_tilda_10_2 = solution(matrix - 10 ** (-2), b - 10 ** (-2))
    print(LA.norm(x - x_tilda_10_2))
    print("Разница между точным и приблизительным решением, измененным на 10^-5:")
    x_tilda_10_5 = solution(matrix - 10 ** (-5), b - 10 ** (-5))
    print(LA.norm(x - x_tilda_10_5))
    print("Разница между точным и приблизительным решением, измененным на 10^-8:")
    x_tilda_10_8 = solution(matrix - 10 ** (-8), b - 10 ** (-8))
    print(LA.norm(x - x_tilda_10_8))
    print('\n')

def main():
    matrix_1 = np.array([[-401.64, 200.12], [21200.72,  -601.76]]) 
    b_1 = np.array([[1400.6], [4198.8]])
    print_solution(matrix_1, b_1)

    matrix_2 = np.array([[-400.94, 200.02], [1200.12, -600.96]])
    b_2 = np.array([[-201.82], [597.36]])
    print_solution(matrix_2, b_2)

    matrix_3 = np.array([[3, -2, 1], [1, -3, 2], [-1, 2, 4]])
    b_3 = np.array([[10], [1], [35]])
    print_solution(matrix_3, b_3)

    matrix_4 = hilbert_matrix(2)
    b_4 = np.array([[6], [3.5]])
    print_solution(matrix_4, b_4)

    matrix_5 = hilbert_matrix(4)
    b_5 = np.array([[37/4], [35/6], [131/30], [123/35]])
    print_solution(matrix_5, b_5)

    matrix_6 = hilbert_matrix(5)
    b_6 = np.array([[49/10], [69/20],[571/210],[79/35],[2437/1260]])
    print_solution(matrix_6, b_6)

main()
