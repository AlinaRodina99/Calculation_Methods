import numpy as np
import pandas as pd
from math import cos,pi

def monte_carlo(N,p="uniform",):
    X = []
    if p == "uniform":
        X = np.random.uniform(a,b,N)
        return ((b-a)/N)*np.sum(np.array([g(x) for x in X]))
    if p == "linear":
        while (len(X)<N):
            point = np.random.uniform(0,1,2)
            x0,y0= a+point[0]*(b-a),point[1]*(4/pi)
            if (0<=y0<=linear_p(x0)):
                X.append(x0)
    X = np.array(X)
    return (1/N)*np.sum(np.array([g(x)/linear_p(x) for x in X]))

def monte_carlo_square(N):
    k=0
    X = np.array([(np.random.uniform(a,b), np.random.uniform(0, 1)) for i in range(N)])
    for i in range(N):
        if X[i,1] <= g(X[i,0]):
            k+=1
    return (k/N)*(pi/2)

def g(x):
    return cos(x)

a,b = 0,pi/2


def linear_p(x):
    return 4/pi-(8*x)/pi**2

def main():
    for i in range(2,6):
        print("Параметр N:", 10**(i))
        print("С использованием равномерной плотности:",abs(1-monte_carlo(10**(i))))
        print("С использованием линейной плотности:",abs(1-monte_carlo(10**(i),"linear")))
        print("Площадь:", abs(1-monte_carlo_square(10**(i))))

main()
