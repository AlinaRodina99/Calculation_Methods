import pandas as pd
import numpy as np
import matplotlib.pyplot  as plt
import math
from numpy.linalg import norm

def grid_solution(a, b, functions, conditions, h):
    alpha0,alpha1,beta0,beta1,A_c,B_c = conditions
    k,p,q,f = functions
    n = round((b-a)/h)
    x = np.float_([a+i*h for i in range(n+1)])
    A = np.float_([0,*[ 2 * k(x[i])-h*p(x[i]) for i in range(1,n)],-beta1])
    B = np.float_([h*alpha0 - alpha1,*[-4 * k(x[i]) + 2 * h * h * q(x[i]) for i in range(1,n)], h*beta0+beta1])
    C = np.float_([alpha1,*[2 * k(x[i]) + h * p(x[i]) for i in range(1,n)],0])
    G = np.float_([h*A_c,*[2 * h * h * f(x[i]) for i in range(1,n)],h*B_c])
    s = np.zeros(n+1, dtype=float)
    t = np.zeros(n+1,dtype=float)
    u = np.zeros(n+1,dtype=float)
    s[0] = -C[0]/B[0]
    t[0] = G[0]/B[0]
    for i in range(1, n + 1):
        s[i] = -C[i]/(A[i]*s[i-1]+B[i])
        t[i] = (G[i]-A[i]*t[i-1])/(A[i]*s[i-1]+B[i])
    u[n] = t[n]
    for i in range(n-1,-1,-1):
        u[i] = s[i]*u[i + 1]+t[i]
    return u

def build_grid(a, b, functions,conditions, h, epsilon):
    condense_coeff = 2
    k = 0
    v2 = grid_solution(a, b, functions,conditions,h)
    while True:
        k += 1
        v1 = v2
        v2 = grid_solution(a, b, functions, conditions,h/(condense_coeff**k))
        err = np.float_([(v2[2*i]-v1[i])/(condense_coeff**1-1) for i in range(v1.shape[0])]) 
        if norm(err) < epsilon:
            for i in range(len(err)):
                if i % 2 == 0:
                    v2[2*i] += err[i]
                else:
                    v2[i] += (err[i - 1] + err[i + 1]) / 2
            x = np.zeros(v2.shape[0], dtype=float)
            for i in range(v2.shape[0]):
                x[i] = a + i * h / (condense_coeff ** k)
            return x, v2, h/(condense_coeff ** k), k

def main():
    print("-1/(x-3)u'' + (1 + x/2)u' + e^x/2 u = 2-x, u(-1)=u(1)=0")
    functions_0 = []
    functions_0.append(lambda x: -1/(x-3))
    functions_0.append(lambda x: 1 + x/2)
    functions_0.append(lambda x: math.e ** (x/2))
    functions_0.append(lambda x: 2-x)
    conditions_0 = []
    conditions_0.append(1)
    conditions_0.append(0)
    conditions_0.append(1)
    conditions_0.append(0)
    conditions_0.append(0)
    conditions_0.append(0)
    fig, axes = plt.subplots(1, 2, figsize=(20, 4))
    i = 0
    for eps in [0.01,0.001]:
        x_sol,y_sol,h_new,k = build_grid(-1,1,functions_0,conditions_0,1/8,eps)
        axes[i].plot(x_sol, y_sol, marker='.', color='blue', mec='black', ms=8)
        axes[i].set_title("текущий epsilon: {}\n шаг сетки: {} \n количество шагов сгущения сетки: {}".format(eps,h_new,k))
        i+=1

    print("-1/(x+3) * u^(2) - xu^(1) + ln(2+x)u = 1 - x/2, u^(1)(-1) = u^(1)(1) + 1/2u(1) = 0")
    functions_1 = []
    functions_1.append(lambda x: -1/(x+3))
    functions_1.append(lambda x: -x)
    functions_1.append(lambda x: math.log(2+x))
    functions_1.append(lambda x: 1-x/2)
    conditions_1 = []
    conditions_1.append(0)
    conditions_1.append(1)
    conditions_1.append(0.5)
    conditions_1.append(0)
    conditions_1.append(0)
    conditions_1.append(0)
    fig, axes = plt.subplots(1, 2, figsize=(20, 4))
    i = 0
    for eps in [0.1,0.01]:
        x_sol,y_sol,h_new,k = build_grid(-1,1,functions_1,conditions_1,1/8,eps)
        axes[i].plot(x_sol, y_sol, marker='.', color='blue', mec='black', ms=8)
        axes[i].set_title("текущий epsilon: {}\n шаг сетки: {} \n количество шагов сгущения сетки: {}".format(eps,h_new,k))
        i+=1

    print("(x-2/x+2) u'' + xu' + (1-sin(x))u = x^2, u(-1)=u(1)=0")
    functions_2 = []
    functions_2.append(lambda x: (x-2)/(x+2))
    functions_2.append(lambda x: x)
    functions_2.append(lambda x: 1 - math.sin(x))
    functions_2.append(lambda x: x ** 2)
    conditions_2 = []
    conditions_2.append(1)
    conditions_2.append(0)
    conditions_2.append(1)
    conditions_2.append(0)
    conditions_2.append(0)
    conditions_2.append(0)

    fig, axes = plt.subplots(1, 2, figsize=(20, 4))
    i = 0
    for eps in [0.0001,0.00001]:
        x_sol,y_sol,h_new,k = build_grid(-1,1,functions_0,conditions_0,1/8,eps)
        axes[i].plot(x_sol, y_sol, marker='.', color='blue', mec='black', ms=8)
        axes[i].set_title("текущий epsilon: {}\n шаг сетки: {} \n количество шагов сгущения сетки: {}".format(eps,h_new,k))
        i+=1

main()