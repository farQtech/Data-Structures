#include<stdio.h>
#include<conio.h>
#include<stdlib.h>

int main()
{
    int a[50],i,size;
printf("Enter the size of the array:");
scanf("%d", &size);

if (size>50)
    {
        printf("Overflow Condition:\n");
        return 0;
    }
else
printf("Enter the elements you want to insert:\n");
for(i=0;i<size;i++);
{   
    scanf("%d", &a[i]);
}
printf("The elements are:");
for(i=0;i<size;i++);
{
    printf("%d",a[i]);  
}
return 0;
}