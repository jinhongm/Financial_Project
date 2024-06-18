import React, { ChangeEvent, SyntheticEvent, useState } from 'react';
import logo from './logo.svg';
import './App.css';
import Card from './Components/Card/Card';
import CardList from './Components/CardList/CardList'
import Search from './Components/Search/Seach'
import { searchCompanies } from './api';
import { CompanySearch } from './company';
import ListPortfolio from './Portfolio/ListPortfolio/ListPortfolio';
import Navbar from './Components/Navbar/Navbar';
import Hero from './Components/Hero/Hero';

function App() {
  // useState<string>("")定义了一个状态变量search和一个更新这个状态的函数setSearch。初始值为一个空字符串""。
  const [search, setSearch] = useState<string>("");
  const [PortfolioValues, setPortfolioValues] = useState<string[]>([]);
  const [searchResult, setSearchResult] = useState<CompanySearch[]>([]);
  const [serverError, setServerError] = useState<string>("");
  // 在handleChange函数中，e.target.value获取到输入框的当前值，并通过setSearch设置为状态变量search的新值。同时，函数中使用console.log输出了这个值，以便于调试和查看变化。
  const handleSearchChange = (e: ChangeEvent<HTMLInputElement>) => {
    // ChangeEvent 用于处理表单元素的值变化，比如输入框（input）、选择框（select）、文本域（textarea）等。
    setSearch(e.target.value);
    console.log(e);
  }
  
  const onPortfolioCreate = (e: any) => {
    e.preventDefault()
    const exists = PortfolioValues.find((value) => value === e.target[0].value);
    if (exists) return;
    // ...（展开运算符）用于将数组或对象展开。它允许我们创建一个新数组或对象，其中包含原始数组或对象的所有元素或属性。
    const updatePortfolio = [...PortfolioValues, e.target[0].value];
    setPortfolioValues(updatePortfolio);
  }

  const onPortfolioDelete = (e: any) => {
    e.preventDefault()
    const removed = PortfolioValues.filter((value) => {
      return value !== e.target[0].value;
    });
    setPortfolioValues(removed)
  }

  const onSearchSubmit = async (e: SyntheticEvent) => {
    e.preventDefault()
    const result = await searchCompanies(search)
    if (typeof result === "string") {
      setServerError(result)
    } else if(Array.isArray(result.data)){
      setSearchResult(result.data)
    }
    // SyntheticEvent 是React的一个跨浏览器的事件封装。
    // MouseEvent 用于处理与鼠标操作相关的事件，如点击（click）、双击（dblclick）、鼠标按下（mousedown）、鼠标放开（mouseup）等。
    console.log(searchResult);
  };

  return (
    <div className="App">
        <Navbar />
        <Hero />
        <Search onSearchSubmit={onSearchSubmit} search={search} handleSearchChange={handleSearchChange}/>
        <ListPortfolio portfolioValues={PortfolioValues} onPortfolioDelete={onPortfolioDelete}/>
        {serverError && <h1>{serverError}</h1>}
        <CardList searchResults={searchResult} onPortfolioCreate={onPortfolioCreate}/>
    </div>
  );

}

export default App;

