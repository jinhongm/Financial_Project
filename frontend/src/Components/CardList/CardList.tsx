import React, { SyntheticEvent } from "react";
import Card from "../Card/Card";
import { CompanySearch } from "../../company";
import { v4 as uuidv4 } from "uuid";

interface Props {
  searchResults: CompanySearch[];
  onPortfolioCreate: (e:SyntheticEvent) => void;
}

/* CardList 组件接收 searchResults，这是一个 CompanySearch 数组。
map 方法遍历 searchResults 数组，并对每个元素执行回调函数。
在回调函数中，result 代表当前元素，即 CompanySearch 对象。
每个 result 被传递给一个 Card 组件，作为 searchResults prop。*/
const CardList: React.FC<Props> = ({ searchResults, onPortfolioCreate }: Props): JSX.Element => {
  return (
    // 5. 使用 Fragment 包裹返回的 JSX
    <>
      {/* 6. 使用三元运算符进行条件渲染 */}
      {searchResults.length > 0 ? (
        // 7. 使用 map 方法遍历 searchResults 数组
        // 在 map 方法中，result 表示 searchResults 数组中的每个元素。然后，这个 result 被传递给 Card 组件作为 searchResults prop。
        searchResults.map((result) => {
          // 8. 使用箭头函数定义 map 方法中的回调函数
          return (
            // 9. 渲染 Card 组件并传递 props
            <Card id={result.symbol} key={uuidv4()} searchResults={result} onPortfolioCreate={onPortfolioCreate}/>
          );
        })
      ) : (
        // 10. 如果没有结果，显示 "No Result"
        <p className="mb-3 mt-3 text-xl font-semibold text-center md:text-xl">
        No results!
      </p>
      )}
    </>
  );
};


export default CardList;