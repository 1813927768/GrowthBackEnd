var daVer = 1,              // 数据库版本号
    db = '',                // 用于数据库对象
    dbData = [];            // 用于临时存放数据的数组

// 连接数据库
function openDB(dbName) {
    var request = indexedDB.open(dbName, daVer);
    request.onsuccess = function (e) {
        db = e.target.result;
        console.log('连接数据库' + dbName + '成功');
        // 数据库连接成功后渲染表格
        //vm.getData();  //vm是创建的Vue实例,getData()是实例的method
    }
    request.onerror = function () {
        console.log('连接数据库' + dbName + '失败');
    }
    request.onupgradeneeded = function (e) {
        db = e.target.result;
        console.log('创建数据库' + dbName + '成功');
        // 如果不存在HisData对象仓库则创建
        if (!db.objectStoreNames.contains('HisData')) {
            // var store = db.createObjectStore('HisData', { keyPath: 'date' });
            if (dbName == "daySumDB") {
                var store = db.createObjectStore('HisData', { keyPath: 'time' });
            }
            else {
                var store = db.createObjectStore('HisData', { keyPath: 'date' });
            }
            //var idx = store.createIndex('index', 'username', { unique: false })
        }
    }
}

/**
 * 保存数据
 * @param {Object} data 要保存到数据库的数据对象
 */
function saveData(data, dbName) {
    //debugger;
    var request = indexedDB.open(dbName, 1);
    request.onsuccess = function (e) {
        console.log('插入：打开数据库' + dbName + '成功');
        db = e.target.result;
        var tx = db.transaction('HisData', 'readwrite');
        var store = tx.objectStore('HisData');
        var req = store.put(data);
        req.onsuccess = function () {
            //console.log('成功保存id为' + this.result + '的数据');
        }
    }
}


/**
 * 读取单条数据
 * @param {Object} index 要读取的主键值
 */
function readData(index, dbName, callback) {
    //debugger;
    var request = indexedDB.open(dbName, 1);
    request.onsuccess = function (e) {
        console.log('读取单条：打开数据库' + dbName + '成功');
        db = e.target.result;
        var tx = db.transaction('HisData', 'readonly');
        var store = tx.objectStore('HisData');
        var req = store.get(index);
        dbData = {};
        req.onsuccess = function () {
            //console.log('成功保存id为' + this.result + '的数据');
            dbData = req.result;
            // debugger
            callback && callback();
        }
        req.onerror = function (event) {
            console.log('读取单条失败');
        };
    }
}



/**
 * 删除单条数据
 * @param {int} id 要删除的数据主键
 */
function deleteOneData(id) {
    var tx = db.transaction('HisData', 'readwrite');
    var store = tx.objectStore('HisData');
    var req = store.delete(id);
    req.onsuccess = function () {
        // 删除数据成功之后重新渲染表格
        vm.getData();
    }
}

/**
 * 检索全部数据
 * @param {function} callback 回调函数
 */
function searchData(startDate, endDate, callback, dbName) {
    var request = indexedDB.open(dbName, 1);
    request.onsuccess = function (e) {
        db = e.target.result;
        console.log('查找：打开数据库' + dbName + '成功');
        var tx = db.transaction('HisData', 'readonly');
        var store = tx.objectStore('HisData');
        var range = IDBKeyRange.bound(startDate, endDate);
        var req = store.openCursor(range, 'next');
        // 每次检索重新清空存放数据数组
        dbData = [];
        req.onsuccess = function () {
            var cursor = this.result;
            if (cursor) {
                // 把检索到的值push到数组中
                dbData.push(cursor.value);
                cursor.continue();
            } else {
                callback & callback();
            }
        }
    }
}

//删除数据库
function deleteDB(dbName) {
    indexedDB.deleteDatabase(dbName);
}
