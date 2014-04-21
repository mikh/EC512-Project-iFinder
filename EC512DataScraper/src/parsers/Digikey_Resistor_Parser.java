package parsers;

import java.io.BufferedWriter;
import java.io.FileWriter;
import java.io.IOException;
import java.util.ArrayList;

import string_operations.StrOps;
import data_structures.Digikey_Item;

public class Digikey_Resistor_Parser {
	private ArrayList<ArrayList<String>> raw_data = new ArrayList<ArrayList<String>>();
	private ArrayList<Digikey_Item> items = new ArrayList<Digikey_Item>();
	private int last_index;
	
	public void load(ArrayList<ArrayList<String>> data){
		raw_data = data;
	}
	
	public void parse(){
		for(int ii = 0; ii < raw_data.size(); ii++){
			Digikey_Item item = new Digikey_Item();
			String partial = "";
			int index = 0;
			String line = raw_data.get(ii).get(0);
			line = StrOps.trimString(line);
			while(index < line.length() && line.charAt(index) != ' '){
				partial += line.charAt(index++);
			}
			item.ID = partial;
			line = raw_data.get(ii).get(1);
			line = StrOps.trimString(line);
			index = 0;
			partial = getNextWord(line, 0);
			if(!partial.equals(item.ID)){
				System.out.println(String.format("Item %d has unmatched ID tags %s = proper tag, %s = given tag",ii,item.ID,partial));
				System.exit(-1);
			}
			
			partial = getNextWord(line, last_index);
			if(!partial.equals("-")){
				System.out.println(String.format("Item %d has unmatched tags %s = proper tag, %s = given tag",ii,"-",partial));
				System.exit(-1);
			}
			
			String str = "";
			partial = getNextWord(line, last_index);
			int iterations = 0;
			while(StrOps.findPattern(partial, "-ND") == -1 && iterations < 20){
				iterations++;
				str += partial;
				str += " ";
				partial = getNextWord(line, last_index);
			}
			if(iterations == 20){
				System.out.println(String.format("Item %d iterated 20 times.",ii));
				System.exit(-1);
			}
			item.manufacturer = str;
			
			partial = getPreviousWord(line, line.length()-1);
			item.availability = partial;
			partial = getPreviousWord(line, last_index);
			if(!partial.equals("-")){
				System.out.println(String.format("Item %d has unmatched tags %s = proper tag, %s = given tag",ii,"-",partial));
				System.exit(-1);
			}
			partial = getPreviousWord(line, last_index);
			String temp = "";
			for(int jj = 0; jj < partial.length(); jj++){
				if(partial.charAt(jj) != ',')
					temp += partial.charAt(jj);
			}
			partial = temp;
				
			try{
				item.quantity = Integer.parseInt(partial);
			} catch(NumberFormatException e){
				System.out.println(String.format("Item %d raised a number format exception error. String = %s",ii,partial));
				System.exit(-1);
			}
			
			line = raw_data.get(ii).get(2);
			line = StrOps.trimString(line);
			partial = getNextWord(line,0);
			try{
				item.price = Double.parseDouble(partial);
			} catch(NumberFormatException e){
				System.out.println(String.format("Item %d raised a number format exception error. String = %s",ii,partial));
				System.exit(-1);
			}
			
			partial = getNextWord(line,last_index);
			try{
				item.minimum_quantity = Integer.parseInt(partial);
			} catch(NumberFormatException e){
				System.out.println(String.format("Item %d raised a number format exception error. String = %s",ii,partial));
				System.exit(-1);
			}
			
			partial = getNextWord(line,last_index);
			item.packaging = partial;
			
			partial = getNextWord(line,last_index);
			item.series = partial;
			
			partial = getNextWord(line, last_index);
			try{
				while(!(partial.charAt(0) >= '0' && partial.charAt(0) <= '9'))
					partial = getNextWord(line, last_index);
			} catch(StringIndexOutOfBoundsException e){
				System.out.println(String.format("Item %d raised a string index out of bounds exception error. String = %s",ii,partial));
				System.exit(-1);
			}
			try{
				int qq = StrOps.findPattern(partial, "k");
				if(qq != -1){
					temp = "";
					for(int jj = 0; jj < partial.length(); jj++){
						if(partial.charAt(jj) != 'k')
							temp += partial.charAt(jj);
					}
					partial = temp;
				}
				item.resistance_ohms = Double.parseDouble(partial);
				if(qq != -1){
					item.resistance_ohms *= 1000;
					//System.out.println(item.resistance_ohms);
				}
			} catch(NumberFormatException e){
				System.out.println(String.format("Item %d raised a number format exception error. String = %s",ii,partial));
				System.exit(-1);
			}
			
			partial = getNextWord(line, last_index);
			try{
				item.tolerance = Integer.parseInt(partial);
			} catch(NumberFormatException e){
				System.out.println(String.format("Item %d raised a number format exception error. String = %s",ii,partial));
				System.exit(-1);
			}
			
			partial = getNextWord(line, last_index);
			int qq = StrOps.findPattern(partial, "W");
			if(qq != -1){
				temp = "";
				for(int jj = 0; jj < partial.length(); jj++){
					if(partial.charAt(jj) != 'W')
						temp += partial.charAt(jj);
				}
				partial = temp;
			}
			try{
				item.power_watts = Double.parseDouble(partial);
			} catch(NumberFormatException e){
				System.out.println(String.format("Item %d raised a number format exception error. String = %s",ii,partial));
				System.exit(-1);
			}
			
			items.add(item);
		}
	//	for(int jj = 0; jj < items.size(); jj++)
		//	System.out.println(items.get(jj).print());
	}
	
	public String getNextWord(String str, int index){
		String nextWord = "";
		while(index < str.length() && (str.charAt(index) == ' ' || str.charAt(index) == '\t'))
			index++;
		while(index < str.length() && str.charAt(index) != ' ' && str.charAt(index) != '\t')
			nextWord += str.charAt(index++);
		last_index = index;
		return nextWord;
	}
	
	public String getPreviousWord(String str, int index){
		String previousWord = "";
		while(index >= 0 && (str.charAt(index) == ' ' || str.charAt(index) == '\t'))
			index--;
		while(index >= 0 && str.charAt(index) != ' ' && str.charAt(index) != '\t')
			previousWord += str.charAt(index--);
		
		String temp = "";
		for(int ii = previousWord.length()-1; ii >= 0; ii--)
			temp += previousWord.charAt(ii);
		previousWord = temp;
		last_index = index;
		return previousWord;
	}
	
	public void printXML(String file){
		try{
			BufferedWriter bw = new BufferedWriter(new FileWriter(file, false));
			bw.write("<data>\n");
			for(int ii = 0; ii < items.size(); ii++)
				bw.write(items.get(ii).printXML());
			bw.write("</data>\n");
			bw.close();
		} catch(IOException e){
			System.out.println(String.format("Digikey_Resistor_Parser::printXML - IOException when trying to write to file %s.", file));
			System.exit(-1);
		}
	}
	
	public void printRaw(){
		for(int ii = 0; ii < raw_data.size(); ii++){
			for(int jj = 0; jj < raw_data.get(ii).size(); jj++)
				System.out.println(raw_data.get(ii).get(jj));
			System.out.print("\n");
		}
	}
	public void printRaw(String file){
		try{
			BufferedWriter bw = new BufferedWriter(new FileWriter(file, false));
			for(int ii = 0; ii < raw_data.size(); ii++){
				for(int jj = 0; jj < raw_data.get(ii).size(); jj++)
					bw.write(raw_data.get(ii).get(jj) + "\n");
				bw.write("\n");
			}
			bw.close();
		} catch(IOException e){
			System.out.println(String.format("Digikey_Resistor_Parser::printRaw - IOException when trying to write to file %s.", file));
			System.exit(-1);
		}
	}
	
	public void printProcessed(){
		for(int ii = 0; ii < items.size(); ii++){
			System.out.println(items.get(ii).print());
		}
	}
	public void printProcessed(String file){
		try{
			BufferedWriter bw = new BufferedWriter(new FileWriter(file, false));
			for(int ii = 0; ii < items.size(); ii++){
				bw.write(items.get(ii).print());
			}
			bw.close();
		} catch(IOException e){
			System.out.println(String.format("Digikey_Resistor_Parser::printProcessed - IOException when trying to write to file %s.", file));
			System.exit(-1);
		}
	}
}
