   M S B A M L   `   `   `  ����F  BDatabaseApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:DatabaseAppDatabaseApp  5   	       DatabaseApp.AccountAdd   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:DatabaseApp     5   	   -   5          � WindowStyle$
  None=�5       � WindowStartupLocation$ CenterOwner=�6-    � 
ResizeMode$ NoResize=�5   	    � Title$ AccountUpdate��5	   	   $	��420��6   $	��460��6,   .��5
      � $��10q�6   
�����F� � RowDefinitions 5   
   �� 5      $��50 �6   �� 5      �� 5      $��50 �6   .+�5   
   � $��0��6   .+�5      �� $��Add new account��6   $��Center=�6.   $��Center=�5      $��30(�6(   
�������66   5      � 5   
   $��1��6   .+�5      �� $��	60,0,60,0q�6   .+�5      �� -   5        $��lblEmail��5      $��Email��6(   $��14(�68   �� 5      -   5        $��tbEmail��5      $��14(�6)   �� 5      -   5        $��lblUsername��5      $��Username��6+   $��14(�6>   �� 5      -   5        $��
tbUsername��5      $��14(�6,   �� 5      -   5        $��
cbPassword��5      $��Password��6-   $��14(�6@   5      �� 5      -   5        $��
tbPassword��5      $��14(�6,   G� 5      -   5        $��
pbPassword��5      $��14(�60   $��	0,-21,0,0q�6>   �� 5       -	   5        $��lblRole��5       $
��Role��6'   $��14(�66   �� 5!      -
   5        $��tbRole��5!      $��14(�6(   �� 5"      -   5        $��lblPhoneNumber��5"      $��Phone Number��6.   $��14(�6E   �� 5#      -   5        $��tbPhoneNumber��5#      $��14(�6/   �� 5$      -   5        $��	lblGender��5$      $��Gender��6)   $��14(�6:   �� 5%      -   5        $��tbGender��5%      $��14(�6*   5&      5'      � 5)   
   $��2��6   $��	60,0,60,0q�6   .+�5*      �� -   5        $��btnSave��5*      $
��Save��6$   $
��Left=�63   $��20,0,0,0q�5+       � Cursor$
 Hand��66   �� 5,      -   5        $��btnClear��5,      $��Clear��6%   $��Center=�65   $��0,0,0,0q�5-      $
 Hand��66   �� 5.      -   5        $��	btnCancel��5.      $��Cancel��6&   $��Right=�67   $��0,0,20,0q�5/      $
 Hand��68   50      51      52      