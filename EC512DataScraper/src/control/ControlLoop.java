package control;

import java.io.BufferedReader;
import java.io.FileNotFoundException;
import java.io.FileReader;
import java.io.IOException;
import java.util.ArrayList;

import parsers.Digikey_Resistor_Parser;
import util.Param;

public class ControlLoop {
	public static void main(String[] args){
		try{
			System.out.println("Start Data Scraper...");
			
			System.out.print("Loading file...");
			BufferedReader br = new BufferedReader(new FileReader(Param.FILE_PATH));
			System.out.println("\tDone.");
			
			System.out.print("Breaking down file into items...");
			ArrayList<ArrayList<String>> raw_text = new ArrayList<ArrayList<String>>();
			ArrayList<String> item = new ArrayList<String>();
			String line;
			while((line=br.readLine()) != null){
				if(!line.equals("	"))
					item.add(line);
				else{
					raw_text.add(item);
					item = new ArrayList<String>();
				}
			}
			br.close();
			System.out.println("\tDone.");
	
			System.out.print("Parse Items...");
			Digikey_Resistor_Parser p = new Digikey_Resistor_Parser();
			p.load(raw_text);
			p.parse();
			p.printXML("data.xml");
			System.out.println("\tDone.");
			
			System.out.println("Operations Complete.");
		} catch (FileNotFoundException e1){
			System.out.println(String.format("File %s could not be found.", Param.FILE_PATH));
			System.exit(-1);
		} catch (IOException e2){
			System.out.println(String.format("Parsing File %s caused an IOException.", Param.FILE_PATH));
			System.exit(-1);
		}
	}
}
