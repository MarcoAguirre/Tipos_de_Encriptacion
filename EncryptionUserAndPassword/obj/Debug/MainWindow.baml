   M S B A M L   `   `   `  ����T  PEncryptionUserAndPassword, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:EncryptionUserAndPasswordEncryptionUserAndPassword  5   	   *    $EncryptionUserAndPassword.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   9local'clr-namespace:EncryptionUserAndPassword     5   	     � Title$  
MainWindow��5   	   $	��300��6   $	��500��6)   .��5	      �  � RowDefinitions 5
   
   �� 5      $��50 �6   �� 5      $	��115 �6   �� 5      �� 5      .+�5   
   �� $"��Put here the text to encrypt��6   $��20(�67   $��Center=�6E   $��Center=�5      �� 5   
   -   5         ��Name$ txtTextToEncrypt��5      $��1��6,   $��Center=�69   $	��Top=�6V   $	��350��5      $	��100��6   �� 5   
   -   5         ��Name$ 
btnEncrypt��5      $��Encrypt��6%   $��Center=�67   $��Center=�6T   $��2��5      $��35��6   $	��100��6*   66   �� 5   
   -   5        $ btnGeneratePrivate��5      $��GO��6-   $��2��6:   $
��Left=�6G   $	��100��6b   6n   �� 5   
   -   5        $ 	txtOutput��5      $
��Auto��6%   $
��Auto��62   $��3��6@   5      5      