#include<stdio.h>

int main()
{
    int a[50],size,i,pos,val;
    printf("Enter the size of array:\n");
    scanf("%d", &size);
    if (size>50 || size<0)
    {
        printf("Invalid size:");
    }
    else
    printf("Enter the %d elements you have to store in array:\n", size);
    for(i=0;i<size;i++)
    {
        scanf("%d", &a[i]);
    }
    printf("Enter the postion at which you have to store the new element:\n");
    scanf("%d", &pos);
    if (pos>size || pos<0)
    {
        printf("Invalid Position:");
    }
    else
    for(i=size-1;i>=pos-1;i--)
    {
        a[i+1] = a[i];
    }
    printf("Enter the value you want to at %d:\n", pos);
    scanf("%d", &val);
    a[pos-1]= val;
    printf("Elements of array after insertion at specific position is:\n");
    for(i=0;i<=size;i++)
    {
        printf("%d\t", a[i]);
    }
    return 0;
}