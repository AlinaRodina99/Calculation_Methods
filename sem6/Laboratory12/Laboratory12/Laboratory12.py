import numpy as np
import matplotlib.pyplot as plt
from numpy.linalg import norm

def calculate_centers(X,clusters):
    ans = []
    for i in np.unique(clusters):
        cluster = X[clusters == i] 
        if cluster is not None:
            ans.append(np.mean(cluster,axis = 0))
    return np.array(ans)

def KMeans(X,centers,metric):
    num_of_iters = 0
    while True:
        num_of_iters +=1
        if metric == "manhattan":
            clusters = np.array([np.argmin(np.sum(np.abs(x-centers),axis=1)) for x in X])   
        else:
            clusters = np.array([np.argmin(norm(x-centers, axis=1)) for x in X])
        centers = calculate_centers(X,clusters)
        if metric == "manhattan":
            diams = np.array([round(np.max(np.sum(np.abs(centers[i]-X[clusters == i]),axis=1)),3) for i in range(len(centers))])
        else:
            diams = np.array([round(np.max(norm(X[clusters == i]-centers[i],axis=1)),3)for i in range(len(centers))])
        return clusters, centers, num_of_iters,diams

def main():
    X = np.random.uniform(low=-40,high=40,size=(80,2))
    k = 4

    centers = [X[np.random.choice(len(X),k)],
           np.array([(max(X[:, 0]), max(X[:, 1])), (min(X[:, 0]), min(X[:, 1])), (max(X[:, 0]), min(X[:, 1])),
                                (min(X[:, 0]), max(X[:, 1]))])]
    metrics = ['euclid','manhattan']

    centers_name = ['random','max/min']
    
    fig, axes = plt.subplots(2,2,figsize=(13,13))

    for i in range(2):
        for j in range(2):
            cls,cent,k,diams = KMeans(X,centers[i],metrics[j])
            axes[i,j].scatter(X[:,0],X[:,1],c=cls)
            axes[i,j].scatter(cent[:,0],cent[:,1],c="red",s=60, marker="D")
            axes[i,j].set_title("Mетрика {};\n Выбор центра: {}; \n Количество итераций: {} \n Диаметры кластеров: {}".format(metrics[i],centers_name[j],k,diams))
   
main()
