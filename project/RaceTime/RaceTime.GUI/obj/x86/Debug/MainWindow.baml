   M S B A M L   `   `   `  ����G  CRaceTime.GUI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:RaceTime.GUIRaceTime.GUI  5   	       RaceTime.GUI.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   *guiclr-namespace:RaceTime.GUI     5   	   -   5          � Title$  
MainWindow��5   	   $	��350��6   $	��525��6)   .��5      � .+�5   
   �� .+�5	      �� -   5         ��Name$ Button1��5	      $��
Start Race��6&   ��6;   �� *StartRaceCommand+5
      �� 5       �� Orientation$ 
Horizontal=�6   .+�5      �� .��6   Race Started:6'   �� 5      -   5         ��Name$ 	CheckBox1��5       d� 	IsChecked 6.   �� *RaceStarted+5      �� 5      $ 
Horizontal=�6   .+�5      �� .��6   Race Elapsed:6'   �� 5      -   5        $ 	CheckBox2��5       6.   �� *RaceElapsed+5      �� 5      -   5         ��Name$ 
TextBlock1��5      $��60(�6,   ��6:   �� *ElapsedTime+�� 5      -   5        $ 
TextBlock2��5      $��60(�6,   ��6:   �� *
RaceTime+5      5      5      