(function($) {
    //流程数组对象，所有数据以JSON形式展现，通过JQuery的extend方法动态修改展现
    var myflow = {};
    //流程配置信息
    myflow.config = {
        editable: true,
        lineHeight: 15,
        basePath: "",
        //矩形
        rect: {
            attr: {
                x: 10,
                y: 10,
                width: 100,
                height: 50,
                r: 5,
                fill: "90-#fff-#C0C0C0",
                stroke: "#000",
                "stroke-width": 1
            },
            showType: "image&text",
            type: "state",
            name: {
                text: "state",
                "font-style": "italic"
            },
            text: {
                text: "状态",
                "font-size": 13
            },
            margin: 5,
            props: [],
            img: {}
        },
        //路径
        path: {
            attr: {
                path: {
                    path: "M10 10L100 100",
                    stroke: "#808080",
                    fill: "none",
                    "stroke-width": 2
                },
                arrow: {
                    path: "M10 10L10 10",
                    stroke: "#808080",
                    fill: "#808080",
                    "stroke-width": 2,
                    radius: 4
                },
                fromDot: {
                    width: 5,
                    height: 5,
                    stroke: "#fff",
                    fill: "#000",
                    cursor: "move",
                    "stroke-width": 2
                },
                toDot: {
                    width: 5,
                    height: 5,
                    stroke: "#fff",
                    fill: "#000",
                    cursor: "move",
                    "stroke-width": 2
                },
                bigDot: {
                    width: 5,
                    height: 5,
                    stroke: "#fff",
                    fill: "#000",
                    cursor: "move",
                    "stroke-width": 2
                },
                smallDot: {
                    width: 5,
                    height: 5,
                    stroke: "#fff",
                    fill: "#000",
                    cursor: "move",
                    "stroke-width": 3
                }
            },
            text: {
                text: "TO {to}",
                cursor: "move",
                background: "#000"
            },
            textPos: {
                x: 0,
                y: -10
            },
            props: {
                text: {
                    name: "text",
                    label: "显示",
                    value: "",
                    editor: function() {
                        return new myflow.editors.textEditor()
                    }
                }
            }
        },
        //工具栏
        tools: {
            attr: {
                left: 10,
                top: 10
            },
            pointer: {},
            path: {},
            states: {},
            save: {
                onclick: function(div) {
                }
            }
        },
        //属性栏
        props: {
            attr: {
                top: 10,
                right: 30
            },
            props: {}
        },
        //数据
        restore: "",
        //当前活动节点
        activeRects: {
            rects: [],
            rectAttr: {
                stroke: "#ff0000",
                "stroke-width": 2
            }
        },
        //历史流转节点
        historyRects: {
            rects: [],
            pathAttr: {
                path: {
                    stroke: "#00ff00"
                },
                arrow: {
                    stroke: "#00ff00",
                    fill: "#00ff00"
                }
            }
        }
    };
    //流程工具方法
    myflow.util = {
        isLine: function(g, f, e) {
            var d, c;
            if ((g.x - e.x) == 0) {
                d = 1
            } else {
                d = (g.y - e.y) / (g.x - e.x)
            }
            c = (f.x - e.x) * d + e.y;
            if ((f.y - c) < 10 && (f.y - c) > -10) {
                f.y = c;
                return true
            }
            return false
        },
        center: function(d, c) {
            return {
                x: (d.x - c.x) / 2 + c.x,
                y: (d.y - c.y) / 2 + c.y
            }
        },
        nextId: (function() {
            var c = 0;
            return function() {
                return ++c
            }
        })(),

        connPoint: function(j, d) {
            var c = d, e = {
                x: j.x + j.width / 2,
                y: j.y + j.height / 2
            };
            var l = (e.y - c.y) / (e.x - c.x);
            l = isNaN(l) ? 0 : l;
            var k = j.height / j.width;
            var h = c.y < e.y ? -1 : 1, f = c.x < e.x ? -1 : 1, g, i;
            if (Math.abs(l) > k && h == -1) {
                g = e.y - j.height / 2;
                i = e.x + h * j.height / 2 / l
            } else {
                if (Math.abs(l) > k && h == 1) {
                    g = e.y + j.height / 2;
                    i = e.x + h * j.height / 2 / l
                } else {
                    if (Math.abs(l) < k && f == -1) {
                        g = e.y + f * j.width / 2 * l;
                        i = e.x - j.width / 2
                    } else {
                        if (Math.abs(l) < k && f == 1) {
                            g = e.y + j.width / 2 * l;
                            i = e.x + j.width / 2
                        }
                    }
                }
            }
            return {
                x: i,
                y: g
            }
        },

        //箭头
        arrow: function(leftPoint, rightPoint, radius) {
            var area = Math.atan2(leftPoint.y - rightPoint.y, rightPoint.x - leftPoint.x) * (180 / Math.PI);

            var x = rightPoint.x - radius * Math.cos(area * (Math.PI / 180));
            var y = rightPoint.y + radius * Math.sin(area * (Math.PI / 180));

            var x1 = x + radius * Math.cos((area + 120) * (Math.PI / 180));
            var y1 = y - radius * Math.sin((area + 120) * (Math.PI / 180));
            var x2 = x + radius * Math.cos((area + 240) * (Math.PI / 180));
            var y2 = y - radius * Math.sin((area + 240) * (Math.PI / 180));

            return [rightPoint, {
                x: x1,
                y: y1
            }, {
                x: x2,
                y: y2
}]
            }
        };

        //********************************
        //矩形
        myflow.rect = function(states, paper) {
            var obj = this;
            var rectId = "rect" + myflow.util.nextId();
            var currNode = $.extend(true, {}, myflow.config.rect, states);
            var paper = paper;
            var point_x;
            var point_y;
            var rect = paper.rect(currNode.attr.x, currNode.attr.y, currNode.attr.width, currNode.attr.height, currNode.attr.r)
				.hide().attr(currNode.attr);
            var image = paper.image(myflow.config.basePath + currNode.img.src, currNode.attr.x + currNode.img.width / 2,
				currNode.attr.y + (currNode.attr.height - currNode.img.height) / 2, currNode.img.width,
				currNode.img.height).hide();
            //name : {text:'<<start>>'}
            var nameText = paper.text(currNode.attr.x + currNode.img.width + (currNode.attr.width - currNode.img.width) / 2,
				currNode.attr.y + myflow.config.lineHeight / 2, currNode.name.text).hide()
				.attr(currNode.name);
            //text : {text:'开始'}
            var textText = paper.text(
				currNode.attr.x + currNode.img.width + (currNode.attr.width - currNode.img.width) / 2,
				currNode.attr.y + (currNode.attr.height - myflow.config.lineHeight) / 2
						+ myflow.config.lineHeight, currNode.text.text).hide().attr(currNode.text);

            //各元素drag事件添加
            rect.drag(function(x, y) {
                offset(x, y);
            }, function() {
                showNode();
            }, function() {
                hidenNode();
            });
            image.drag(function(x, y) {
                offset(x, y);
            }, function() {
                showNode();
            }, function() {
                hidenNode();
            });
            nameText.drag(function(x, y) {
                offset(x, y);
            }, function() {
                showNode();
            }, function() {
                hidenNode();
            });
            textText.drag(function(x, y) {
                offset(x, y);
            }, function() {
                showNode();
            }, function() {
                hidenNode();
            });

            //偏移位置计算
            var offset = function(x, y) {
                if (!myflow.config.editable) {
                    return
                }
                var newX = (point_x + x);
                var newY = (point_y + y);
                nodeAttr.x = newX - currNode.margin;
                nodeAttr.y = newY - currNode.margin;
                resetNode();
            };

            //显示节点
            var showNode = function() {
                point_x = rect.attr("x");
                point_y = rect.attr("y");
                rect.attr({
                    opacity: 0.5
                });
                image.attr({
                    opacity: 0.5
                });
                textText.attr({
                    opacity: 0.5
                })
            };

            //隐藏节点
            var hidenNode = function() {
                rect.attr({
                    opacity: 1
                });
                image.attr({
                    opacity: 1
                });
                textText.attr({
                    opacity: 1
                })
            };

            var rectArray = {};
            //选中节点周围矩形线的尺寸
            var selectLineSize = 5;
            //节点的位置和尺寸属性
            var nodeAttr = {
                x: currNode.attr.x - currNode.margin,
                y: currNode.attr.y - currNode.margin,
                width: currNode.attr.width + currNode.margin * 2,
                height: currNode.attr.height + currNode.margin * 2
            };

            //各种元素尺寸变化逻辑
            var path = paper.path("M0 0L1 1").hide();
            rectArray.t = paper.rect(0, 0, selectLineSize, selectLineSize).attr({
                fill: "#000",
                stroke: "#fff",
                cursor: "s-resize"
            }).hide().drag(function(w, h) {
                resetSize(w, h, "t");
            }, function() {
                resetPosition(this.attr("x") + selectLineSize / 2, this.attr("y") + selectLineSize / 2, "t")
            }, function() {
            });
            rectArray.lt = paper.rect(0, 0, selectLineSize, selectLineSize).attr({
                fill: "#000",
                stroke: "#fff",
                cursor: "nw-resize"
            }).hide().drag(function(w, h) {
                resetSize(w, h, "lt");
            }, function() {
                resetPosition(this.attr("x") + selectLineSize / 2, this.attr("y") + selectLineSize / 2, "lt")
            }, function() {
            });
            rectArray.l = paper.rect(0, 0, selectLineSize, selectLineSize).attr({
                fill: "#000",
                stroke: "#fff",
                cursor: "w-resize"
            }).hide().drag(function(w, h) {
                resetSize(w, h, "l");
            }, function() {
                resetPosition(this.attr("x") + selectLineSize / 2, this.attr("y") + selectLineSize / 2, "l")
            }, function() {
            });
            rectArray.lb = paper.rect(0, 0, selectLineSize, selectLineSize).attr({
                fill: "#000",
                stroke: "#fff",
                cursor: "sw-resize"
            }).hide().drag(function(w, h) {
                resetSize(w, h, "lb");
            }, function() {
                resetPosition(this.attr("x") + selectLineSize / 2, this.attr("y") + selectLineSize / 2, "lb")
            }, function() {
            });
            rectArray.b = paper.rect(0, 0, selectLineSize, selectLineSize).attr({
                fill: "#000",
                stroke: "#fff",
                cursor: "s-resize"
            }).hide().drag(function(w, h) {
                resetSize(w, h, "b");
            }, function() {
                resetPosition(this.attr("x") + selectLineSize / 2, this.attr("y") + selectLineSize / 2, "b")
            }, function() {
            });
            rectArray.rb = paper.rect(0, 0, selectLineSize, selectLineSize).attr({
                fill: "#000",
                stroke: "#fff",
                cursor: "se-resize"
            }).hide().drag(function(w, h) {
                resetSize(w, h, "rb");
            }, function() {
                resetPosition(this.attr("x") + selectLineSize / 2, this.attr("y") + selectLineSize / 2, "rb")
            }, function() {
            });
            rectArray.r = paper.rect(0, 0, selectLineSize, selectLineSize).attr({
                fill: "#000",
                stroke: "#fff",
                cursor: "w-resize"
            }).hide().drag(function(w, h) {
                resetSize(w, h, "r");
            }, function() {
                resetPosition(this.attr("x") + selectLineSize / 2, this.attr("y") + selectLineSize / 2, "r")
            }, function() {
            });
            rectArray.rt = paper.rect(0, 0, selectLineSize, selectLineSize).attr({
                fill: "#000",
                stroke: "#fff",
                cursor: "ne-resize"
            }).hide().drag(function(w, h) {
                resetSize(w, h, "rt");
            }, function() {
                resetPosition(this.attr("x") + selectLineSize / 2, this.attr("y") + selectLineSize / 2, "rt")
            }, function() {
            });


            //重置各类型节点元素大小尺寸
            var resetSize = function(w, h, type) {
                if (!myflow.config.editable) {
                    return
                }
                var W = _bx + w;
                var H = _by + h;
                switch (type) {
                    case "t":
                        nodeAttr.height += nodeAttr.y - H;
                        nodeAttr.y = H;
                        break;
                    case "lt":
                        nodeAttr.width += nodeAttr.x - W;
                        nodeAttr.height += nodeAttr.y - H;
                        nodeAttr.x = W;
                        nodeAttr.y = H;
                        break;
                    case "l":
                        nodeAttr.width += nodeAttr.x - W;
                        nodeAttr.x = W;
                        break;
                    case "lb":
                        nodeAttr.height = H - nodeAttr.y;
                        nodeAttr.width += nodeAttr.x - W;
                        nodeAttr.x = W;
                        break;
                    case "b":
                        nodeAttr.height = H - nodeAttr.y;
                        break;
                    case "rb":
                        nodeAttr.height = H - nodeAttr.y;
                        nodeAttr.width = W - nodeAttr.x;
                        break;
                    case "r":
                        nodeAttr.width = W - nodeAttr.x;
                        break;
                    case "rt":
                        nodeAttr.width = W - nodeAttr.x;
                        nodeAttr.height += nodeAttr.y - H;
                        nodeAttr.y = H;
                        break
                }
                resetNode();
            };

            //记录尺寸更改后X、Y坐标
            var resetPosition = function(x, y, F) {
                _bx = x;
                _by = y
            };

            //添加个元素连线方法
            $([rect.node, textText.node, nameText.node, image.node]).bind("click", function() {
                if (!myflow.config.editable) {
                    return
                }
                showPath();
                var mod = $(paper).data("mod");
                switch (mod) {
                    case "pointer":
                        break;
                    case "path":
                        var rect = $(paper).data("currNode");
                        if (rect && rect.getId() != rectId
							&& rect.getId().substring(0, 4) == "rect") {
                            $(paper).trigger("addpath", [rect, obj])
                        }
                        break
                }
                $(paper).trigger("click", obj);
                $(paper).data("currNode", obj);
                return false;
            });

            //切换选中节点，属性更改、周围选择提示框更改
            var changeNode = function(event, node) {
                if (!myflow.config.editable) {
                    return
                }
                if (node.getId() == rectId) {
                    $(paper).trigger("showprops", [currNode.props, node])
                } else {
                    hidePath();
                }
            };
            $(paper).bind("click", changeNode);

            //节点text值更改
            var textChange = function(event, newText, node) {
                if (node.getId() == rectId) {
                    textText.attr({
                        text: newText
                    })
                }
            };
            $(paper).bind("textchange", textChange);

            //创建连线
            function createPath() {
                return "M" + nodeAttr.x + " " + nodeAttr.y + "L" + nodeAttr.x + " " + (nodeAttr.y + nodeAttr.height)
					+ "L" + (nodeAttr.x + nodeAttr.width) + " " + (nodeAttr.y + nodeAttr.height) + "L"
					+ (nodeAttr.x + nodeAttr.width) + " " + nodeAttr.y + "L" + nodeAttr.x + " " + nodeAttr.y
            }

            //选中节点周围框显示
            function showPath() {
                path.show();
                for (var rectId in rectArray) {
                    rectArray[rectId].show();
                }
            }

            //选中节点周围框隐藏
            function hidePath() {
                path.hide();
                for (var rectId in rectArray) {
                    rectArray[rectId].hide();
                }
            }

            //节点状态更改，重置属性并显示
            function resetNode() {
                var x = nodeAttr.x + currNode.margin;
                var r = nodeAttr.y + currNode.margin;
                var width = nodeAttr.width - currNode.margin * 2;
                var height = nodeAttr.height - currNode.margin * 2;
                rect.attr({
                    x: x,
                    y: r,
                    width: width,
                    height: height
                });
                //根据不同显示配置，显示图形、文本或图形+文本
                switch (currNode.showType) {
                    case "image":
                        image.attr({
                            x: x + (width - currNode.img.width) / 2,
                            y: r + (height - currNode.img.height) / 2
                        }).show();
                        break;
                    case "text":
                        rect.show();
                        textText.attr({
                            x: x + width / 2,
                            y: r + height / 2
                        }).show();
                        break;
                    case "image&text":
                        rect.show();
                        nameText.attr({
                            x: x + currNode.img.width + (width - currNode.img.width) / 2,
                            y: r + myflow.config.lineHeight / 2
                        }).show();

                        textText.attr({
                            x: x + currNode.img.width + (width - currNode.img.width) / 2,
                            y: r + (height - myflow.config.lineHeight) / 2 + myflow.config.lineHeight
                        }).show();

                        image.attr({
                            x: x + currNode.img.width / 2,
                            y: r + (height - currNode.img.height) / 2
                        }).show();
                        break;
                }

                rectArray.t.attr({
                    x: nodeAttr.x + nodeAttr.width / 2 - selectLineSize / 2,
                    y: nodeAttr.y - selectLineSize / 2
                });
                rectArray.lt.attr({
                    x: nodeAttr.x - selectLineSize / 2,
                    y: nodeAttr.y - selectLineSize / 2
                });
                rectArray.l.attr({
                    x: nodeAttr.x - selectLineSize / 2,
                    y: nodeAttr.y - selectLineSize / 2 + nodeAttr.height / 2
                });
                rectArray.lb.attr({
                    x: nodeAttr.x - selectLineSize / 2,
                    y: nodeAttr.y - selectLineSize / 2 + nodeAttr.height
                });
                rectArray.b.attr({
                    x: nodeAttr.x - selectLineSize / 2 + nodeAttr.width / 2,
                    y: nodeAttr.y - selectLineSize / 2 + nodeAttr.height
                });
                rectArray.rb.attr({
                    x: nodeAttr.x - selectLineSize / 2 + nodeAttr.width,
                    y: nodeAttr.y - selectLineSize / 2 + nodeAttr.height
                });
                rectArray.r.attr({
                    x: nodeAttr.x - selectLineSize / 2 + nodeAttr.width,
                    y: nodeAttr.y - selectLineSize / 2 + nodeAttr.height / 2
                });
                rectArray.rt.attr({
                    x: nodeAttr.x - selectLineSize / 2 + nodeAttr.width,
                    y: nodeAttr.y - selectLineSize / 2
                });
                path.attr({
                    path: createPath()
                });
                $(paper).trigger("rectresize", obj);
            }

            //流程数据转JSON字符串
            this.toJson = function() {
                var jsonStr = "{type:'" + currNode.type + "',text:{text:'" + textText.attr("text")
					+ "'}, attr:{ x:" + Math.round(rect.attr("x")) + ", y:"
					+ Math.round(rect.attr("y")) + ", width:"
					+ Math.round(rect.attr("width")) + ", height:"
					+ Math.round(rect.attr("height")) + "}, props:{";
                for (var prop in currNode.props) {
                    jsonStr += prop + ":{value:'" + currNode.props[prop].value + "'},";
                }
                if (jsonStr.substring(jsonStr.length - 1, jsonStr.length) == ",") {
                    jsonStr = jsonStr.substring(0, jsonStr.length - 1)
                }
                jsonStr += "}}";
                return jsonStr;
            };

            this.restore = function(node) {
                currNode = $.extend(true, currNode, node);
                textText.attr({
                    text: node.text.text
                });
                resetNode();
            };

            this.getBBox = function() {
                return nodeAttr;
            };

            this.getId = function() {
                return rectId;
            };

            //各元素移除
            this.remove = function() {
                rect.remove();
                textText.remove();
                nameText.remove();
                image.remove();
                path.remove();
                for (var rectId in rectArray) {
                    rectArray[rectId].remove()
                }
            };

            this.text = function() {
                return textText.attr("text")
            };

            this.attr = function(args) {
                if (args) {
                    rect.attr(args)
                }
            };

            resetNode();
        };

        //***************

        //连线
        myflow.path = function(q, paper, fromEvent, toEvent) {
            var obj = this;
            var paper = paper;
            var pathObj;
            var path = $.extend(true, {}, myflow.config.path);
            var textPos = path.textPos;
            var pathId = "path" + myflow.util.nextId();
            var x;
            var y;
            var _path;
            var _arrow;
            var _pathText;

            function p(type, centerNode, leftNode, rightNode) {
                var obj = this;
                var rect;
                var X;
                var Y;
                var cNode = centerNode;
                switch (type) {
                    case "from":
                        rect = paper.rect(centerNode.x - path.attr.fromDot.width / 2,
							centerNode.y - path.attr.fromDot.height / 2,
							path.attr.fromDot.width, path.attr.fromDot.height)
							.attr(path.attr.fromDot);
                        break;
                    case "big":
                        rect = paper.rect(centerNode.x - path.attr.bigDot.width / 2,
							centerNode.y - path.attr.bigDot.height / 2,
							path.attr.bigDot.width, path.attr.bigDot.height)
							.attr(path.attr.bigDot);
                        break;
                    case "small":
                        rect = paper.rect(centerNode.x - path.attr.smallDot.width / 2,
							centerNode.y - path.attr.smallDot.height / 2,
							path.attr.smallDot.width, path.attr.smallDot.height)
							.attr(path.attr.smallDot);
                        break;
                    case "to":
                        rect = paper.rect(centerNode.x - path.attr.toDot.width / 2,
							centerNode.y - path.attr.toDot.height / 2, path.attr.toDot.width,
							path.attr.toDot.height).attr(path.attr.toDot);
                        break
                }
                if (rect && (type == "big" || type == "small")) {
                    rect.drag(function(_x, _y) {
                        movo(_x, _y);
                    }, function() {
                        setPosition();
                    }, function() {
                    });

                    var movo = function(_x, _y) {
                        var _X = (X + _x);
                        var _Y = (Y + _y);
                        obj.moveTo(_X, _Y);
                    };

                    var setPosition = function() {
                        if (type == "big") {
                            X = rect.attr("x") + path.attr.bigDot.width / 2;
                            Y = rect.attr("y") + path.attr.bigDot.height / 2
                        }
                        if (type == "small") {
                            X = rect.attr("x") + path.attr.smallDot.width / 2;
                            Y = rect.attr("y") + path.attr.smallDot.height / 2
                        }
                    };
                }
                this.type = function(P) {
                    if (P) {
                        type = P
                    } else {
                        return type;
                    }
                };
                this.node = function(P) {
                    if (P) {
                        rect = P
                    } else {
                        return rect
                    }
                };
                this.left = function(P) {
                    if (P) {
                        leftNode = P
                    } else {
                        return leftNode
                    }
                };
                this.right = function(P) {
                    if (P) {
                        rightNode = P
                    } else {
                        return rightNode
                    }
                };
                this.remove = function() {
                    leftNode = null;
                    rightNode = null;
                    rect.remove()
                };
                this.pos = function(P) {
                    if (P) {
                        cNode = P;
                        rect.attr({
                            x: cNode.x - rect.attr("width") / 2,
                            y: cNode.y - rect.attr("height") / 2
                        });
                        return this
                    } else {
                        return cNode
                    }
                };
                this.moveTo = function(x, y) {
                    this.pos({
                        x: x,
                        y: y
                    });
                    switch (type) {
                        case "from":
                            if (rightNode && rightNode.right() && rightNode.right().type() == "to") {
                                rightNode.right().pos(myflow.util.connPoint(toEvent.getBBox(), cNode))
                            }
                            if (rightNode && rightNode.right()) {
                                rightNode.pos(myflow.util.center(cNode, rightNode.right().pos()))
                            }
                            break;
                        case "big":
                            if (rightNode && rightNode.right() && rightNode.right().type() == "to") {
                                rightNode.right().pos(myflow.util.connPoint(toEvent.getBBox(), cNode))
                            }
                            if (leftNode && leftNode.left() && leftNode.left().type() == "from") {
                                leftNode.left().pos(myflow.util.connPoint(fromEvent.getBBox(), cNode))
                            }
                            if (rightNode && rightNode.right()) {
                                rightNode.pos(myflow.util.center(cNode, rightNode.right().pos()))
                            }
                            if (leftNode && leftNode.left()) {
                                leftNode.pos(myflow.util.center(cNode, leftNode.left().pos()))
                            }
                            var S = {
                                x: cNode.x,
                                y: cNode.y
                            };
                            if (myflow.util.isLine(leftNode.left().pos(), S, rightNode.right().pos())) {
                                type = "small";
                                rect.attr(path.attr.smallDot);
                                this.pos(S);
                                var P = leftNode;
                                leftNode.left().right(leftNode.right());
                                leftNode = leftNode.left();
                                P.remove();
                                var R = rightNode;
                                rightNode.right().left(rightNode.left());
                                rightNode = rightNode.right();
                                R.remove()
                            }
                            break;
                        case "small":
                            if (leftNode && rightNode && !myflow.util.isLine(leftNode.pos(), {
                                x: cNode.x,
                                y: cNode.y
                            }, rightNode.pos())) {
                                type = "big";
                                rect.attr(path.attr.bigDot);
                                var P = new p("small", myflow.util.center(leftNode.pos(), cNode),
									leftNode, leftNode.right());
                                leftNode.right(P);
                                leftNode = P;
                                var R = new p("small", myflow.util.center(rightNode.pos(), cNode), rightNode
											.left(), rightNode);
                                rightNode.left(R);
                                rightNode = R
                            }
                            break;
                        case "to":
                            if (leftNode && leftNode.left() && leftNode.left().type() == "from") {
                                leftNode.left().pos(myflow.util.connPoint(fromEvent.getBBox(), cNode))
                            }
                            if (leftNode && leftNode.left()) {
                                leftNode.pos(myflow.util.center(cNode, leftNode.left().pos()))
                            }
                            break
                    }
                    drawPath();
                }
            }

            //创建路径对象
            function createPath() {
                var fromDot;
                var toDot;
                var fromBBox = fromEvent.getBBox();
                var toBBox = toEvent.getBBox();
                var r;
                var o;
                r = myflow.util.connPoint(fromBBox, {
                    x: toBBox.x + toBBox.width / 2,
                    y: toBBox.y + toBBox.height / 2
                });
                o = myflow.util.connPoint(toBBox, r);
                fromDot = new p("from", r, null, new p("small", {
                    x: (r.x + o.x) / 2,
                    y: (r.y + o.y) / 2
                }));
                fromDot.right().left(fromDot);
                toDot = new p("to", o, fromDot.right(), null);
                fromDot.right().right(toDot);

                this.toPathString = function() {
                    if (!fromDot) {
                        return "";
                    }
                    var fromDotTemp = fromDot;
                    var pashStr = "M" + fromDotTemp.pos().x + " " + fromDotTemp.pos().y;
                    var str = "";
                    while (fromDotTemp.right()) {
                        fromDotTemp = fromDotTemp.right();
                        pashStr += "L" + fromDotTemp.pos().x + " " + fromDotTemp.pos().y
                    }
                    var arrowArry = myflow.util.arrow(fromDotTemp.left().pos(), fromDotTemp.pos(),
						path.attr.arrow.radius);
                    str = "M" + arrowArry[0].x + " " + arrowArry[0].y + "L" + arrowArry[1].x + " " + arrowArry[1].y
						+ "L" + arrowArry[2].x + " " + arrowArry[2].y + "z";
                    return [pashStr, str];
                };

                //json字符串拼装
                this.toJson = function() {
                    var jsonStr = "[";
                    var fromDot = fromDot;
                    while (fromDot) {
                        if (fromDot.type() == "big") {
                            jsonStr += "{x:" + Math.round(fromDot.pos().x) + ",y:"
								+ Math.round(fromDot.pos().y) + "},"
                        }
                        fromDot = fromDot.right()
                    }
                    if (jsonStr.substring(jsonStr.length - 1, jsonStr.length) == ",") {
                        jsonStr = jsonStr.substring(0, jsonStr.length - 1)
                    }
                    jsonStr += "]";
                    return jsonStr;
                };

                this.restore = function(obj) {
                    var obj = obj;
                    var rightDot = fromDot.right();
                    for (var i = 0; i < obj.length; i++) {
                        rightDot.moveTo(obj[i].x, obj[i].y);
                        rightDot.moveTo(obj[i].x, obj[i].y);
                        rightDot = rightDot.right();
                    }
                    this.hide();
                };

                this.fromDot = function() {
                    return fromDot;
                };
                this.toDot = function() {
                    return toDot;
                };

                this.midDot = function() {
                    var midDot = fromDot.right();
                    var rrDot = fromDot.right().right();
                    while (rrDot.right() && rrDot.right().right()) {
                        rrDot = rrDot.right().right();
                        midDot = midDot.right();
                    }
                    return midDot;
                };

                this.show = function() {
                    var dot = fromDot;
                    while (dot) {
                        dot.node().show();
                        dot = dot.right();
                    }
                };
                this.hide = function() {
                    var dot = fromDot;
                    while (dot) {
                        dot.node().hide();
                        dot = dot.right();
                    }
                };
                this.remove = function() {
                    var dot = fromDot;
                    while (dot) {
                        if (dot.right()) {
                            dot = dot.right();
                            dot.left().remove();
                        } else {
                            dot.remove();
                            dot = null;
                        }
                    }
                }
            }

            B = $.extend(true, path, q);
            _path = paper.path(path.attr.path.path).attr(path.attr.path);
            _arrow = paper.path(path.attr.arrow.path).attr(path.attr.arrow);
            pathObj = new createPath();
            pathObj.hide();
            _pathText = paper.text(0, 0, path.text.text).attr(path.text).attr({
                text: path.text.text.replace("{from}", fromEvent.text()).replace("{to}",
					toEvent.text())
            });
            _pathText.drag(function(r, o) {
                if (!myflow.config.editable) {
                    return
                }
                _pathText.attr({
                    x: x + r,
                    y: y + o
                })
            }, function() {
                x = _pathText.attr("x");
                y = _pathText.attr("y")
            }, function() {
                var o = pathObj.midDot().pos();
                textPos = {
                    x: _pathText.attr("x") - o.x,
                    y: _pathText.attr("y") - o.y
                }
            });
            drawPath();
            $([_path.node, _arrow.node]).bind("click", function() {
                if (!myflow.config.editable) {
                    return
                }
                $(paper).trigger("click", obj);
                $(paper).data("currNode", obj);
                return false
            });

            //点击连线，显示属性和周围选择提示框
            var pathClick = function(r, toDot) {
                if (!myflow.config.editable) {
                    return
                }
                if (toDot && toDot.getId() == pathId) {
                    pathObj.show();
                    $(paper).trigger("showprops", [path.props, obj])
                } else {
                    pathObj.hide()
                }
                var o = $(paper).data("mod");
                switch (o) {
                    case "pointer":
                        break;
                    case "path":
                        break
                }
            };
            $(paper).bind("click", pathClick);


            //删除矩形
            var removeRect = function(event, rect) {
                if (!myflow.config.editable) {
                    return;
                }
                if (rect && (rect.getId() == fromEvent.getId() || rect.getId() == toEvent.getId())) {
                    $(paper).trigger("removepath", obj)  //删除关联路径
                }
            };
            $(paper).bind("removerect", removeRect);

            var rectreSize = function(event, fromDot) {
                if (!myflow.config.editable) {
                    return;
                }

                try {
                    if (fromEvent && fromEvent.getId() == fromDot.getId()) {
                        var o;

                        if (pathObj.fromDot().right().right().type() == "to") {
                            o = {
                                x: toEvent.getBBox().x + toEvent.getBBox().width / 2,
                                y: toEvent.getBBox().y + toEvent.getBBox().height / 2
                            }
                        }
                        else {
                            o = pathObj.fromDot().right().right().pos()
                        }
                        var r = myflow.util.connPoint(fromEvent.getBBox(), o);
                        pathObj.fromDot().moveTo(r.x, r.y);
                        drawPath();
                    }
                    if (toEvent && toEvent.getId() == fromDot.getId()) {
                        var o;
                        if (pathObj.toDot().left().left().type() == "from") {
                            o = {
                                x: fromEvent.getBBox().x + fromEvent.getBBox().width / 2,
                                y: fromEvent.getBBox().y + fromEvent.getBBox().height / 2
                            }
                        } else {
                            o = pathObj.toDot().left().left().pos()
                        }
                        var r = myflow.util.connPoint(toEvent.getBBox(), o);
                        pathObj.toDot().moveTo(r.x, r.y);
                        drawPath();
                    }
                }
                catch (e) {

                }
            };
            $(paper).bind("rectresize", rectreSize);

            var textChange = function(event, newText, path) {
                if (path.getId() == pathId) {
                    _pathText.attr({
                        text: newText
                    })
                }
            };
            $(paper).bind("textchange", textChange);

            this.from = function() {
                return fromEvent;
            };
            this.to = function() {
                return toEvent;
            };
            this.toJson = function() {
                var jsonStr = "{from:'" + fromEvent.getId() + "',to:'" + toEvent.getId() + "', dots:"
					+ pathObj.toJson() + ",text:{text:'" + _pathText.attr("text")
					+ "'},textPos:{x:" + Math.round(textPos.x) + ",y:"
					+ Math.round(textPos.y) + "}, props:{";
                for (var propName in path.props) {
                    if (path.props[propName].condition != null) {
                        jsonStr += propName + ":{value:'" + path.props[propName].value + "',condition:'" + path.props[propName].condition + "'},";
                    }
                    else {
                        jsonStr += propName + ":{value:'" + path.props[propName].value + "'},";
                    }
                }
                if (jsonStr.substring(jsonStr.length - 1, jsonStr.length) == ",") {
                    jsonStr = jsonStr.substring(0, jsonStr.length - 1)
                }
                jsonStr += "}}";
                return jsonStr;
            };
            this.restore = function(o) {
                var r = o;
                path = $.extend(true, path, o);
                pathObj.restore(r.dots)
            };
            this.remove = function() {
                pathObj.remove();
                _path.remove();
                _arrow.remove();
                _pathText.remove();
                try {
                    $(paper).unbind("click", l)
                } catch (o) {
                }
                try {
                    $(paper).unbind("removerect", A)
                } catch (o) {
                }
                try {
                    $(paper).unbind("rectresize", d);
                } catch (o) {
                }
                try {
                    $(paper).unbind("textchange", c)
                } catch (o) {
                }
            };

            function drawPath() {
                var pathStr = pathObj.toPathString();
                var pos = pathObj.midDot().pos();
                _path.attr({
                    path: pathStr[0]
                });
                _arrow.attr({
                    path: pathStr[1]
                });
                _pathText.attr({
                    x: pos.x + textPos.x,
                    y: pos.y + textPos.y
                })
            }

            this.getId = function() {
                return pathId;
            };
            this.text = function() {
                return _pathText.attr("text");
            };
            this.attr = function(o) {
                if (o && o.path) {
                    _path.attr(o.path)
                }
                if (o && o.arrow) {
                    _arrow.attr(o.arrow)
                }
            }
        };

        //显示属性编辑框
        myflow.props = function(event, paper) {
            var div = $("#myflow_props").hide().draggable({
                handle: "#myflow_props_handle"
            }).resizable().css(myflow.config.props.attr).bind("click",
				function() {
				    return false
				});
            var table = div.find("table");
            var currNode;

            var showprops = function(event, states, node) {
                if (currNode && currNode.getId() == node.getId()) {
                    return
                }
                currNode = node;
                $(table).find(".editor").each(function() {
                    var editor = $(this).data("editor");
                    if (editor) {
                        editor.destroy();
                    }
                });
                table.empty();
                div.show();

                if (Content_states != null && states.WorkflowName != null) {                  
                    $.extend(true, states, Content_states.props.props);
                }

                for (var state in states) {
                    table.append("<tr><th>" + states[state].label + '</th><td><div id="p' + state
						+ '" class="editor"></div></td></tr>');
                    if (states[state].editor) {
                        states[state].editor().init(states, state, "p" + state, node, paper);
                    }
                }
            };
            $(paper).bind("showprops", showprops);
        };

        //创建编辑框
        myflow.editors = {
            textEditor: function() {
                var _props, _k, _div, _src, _r;
                this.init = function(props, k, div, src, r) {
                    _props = props;
                    _k = k;
                    _div = div;
                    _src = src;
                    _r = r;
                    $('<input  style="width:100%;"/>').val(_src.text()).change(
						function() {
						    _path[_k].value = $(this).val();
						    $(_r).trigger("textchange", [$(this).val(), _src])
						}).appendTo("#" + _div);
                    $("#" + _div).data("editor", this)
                };
                this.destroy = function() {
                    $("#" + _div + " input").each(function() {
                        _props[_k].value = $(this).val();
                        $(_r).trigger("textchange", [$(this).val(), _src])
                    })
                }
            }
        };

        //myflow对象初始化
        //DOMObj:页面元素 <div id='myflow'></div>
        //myflowObj:myflow对象
        myflow.init = function(DOMObj, myflowObj) {
            var width = $(window).width();
            var height = $(window).height();
            //Raphael画图对象
            var paper = Raphael(DOMObj, width - 300, height);
            //当前图形中矩形对象集合
            var rectArray = {};
            var pathArray = {};
            $.extend(true, myflow.config, myflowObj);
            $(document).keydown(function(event) {
                if (!myflow.config.editable) {
                    return
                }
                //删除元素
                if (event.keyCode == 46) {
                    var currNode = $(paper).data("currNode");
                    if (currNode) {
                        if (currNode.getId().substring(0, 4) == "rect") {
                            $(paper).trigger("removerect", currNode); //删除当前矩形
                        } else {
                            if (currNode.getId().substring(0, 4) == "path") {
                                $(paper).trigger("removepath", currNode); //删除当前路径
                            }
                        }
                        $(paper).removeData("currNode");
                    }
                }
            });
            //元素点击
            $(document).click(function() {
                $(paper).data("currNode", null);
                $(paper).trigger("click", {
                    getId: function() {
                        return "00000000";
                    }
                });
                $(paper).trigger("showprops", [myflow.config.props.props, {
                    getId: function() {
                        return "00000000";
                    }
}])

                });

                //移除元素方法
                var removeNode = function(event, currNode) {
                    if (!myflow.config.editable) {
                        return;
                    }
                    if (currNode.getId().substring(0, 4) == "rect") {//矩形
                        rectArray[currNode.getId()] = null; //置空当前矩形对象集合
                        currNode.remove(); //移除对象
                    } else {
                        if (currNode.getId().substring(0, 4) == "path") {//路径
                            pathArray[currNode.getId()] = null; //置空当前路径对象集合
                            currNode.remove(); //移除对象
                        }
                    }
                };
                //绑定移除事件
                $(paper).bind("removepath", removeNode);
                $(paper).bind("removerect", removeNode);
                //绑定添加矩形事件
                //type:矩形类型，如task，start，end等，attr:x、y坐标
                $(paper).bind("addrect", function(currNode, type, attr) {
                    //创建矩形对象
                    var rect = new myflow.rect($.extend(true, {}, myflow.config.tools.states[type], attr), paper);
                    //将矩形对象放入集合中
                    rectArray[rect.getId()] = rect;
                });
                //添加路径
                var addPath = function(event, node, currNode) {
                    //创建路径对象
                    var path = new myflow.path({}, paper, node, currNode);
                    //将路径对象放入集合中
                    pathArray[path.getId()] = path;
                };
                //绑定添加路径事件
                $(paper).bind("addpath", addPath);

                //工具栏添加拖动、鼠标经过等事件、CSS
                $(paper).data("mod", "point");
                if (myflow.config.editable) {
                    $("#myflow_tools").draggable({
                        handle: "#myflow_tools_handle"
                    }).css(myflow.config.tools.attr);
                    $("#myflow_tools .node").hover(function() {
                        $(this).addClass("mover")
                    }, function() {
                        $(this).removeClass("mover")
                    });
                    $("#myflow_tools .selectable").click(function() {
                        $(".selected").removeClass("selected");
                        $(this).addClass("selected");
                        $(paper).data("mod", this.id)
                    });
                    $("#myflow_tools .state").each(function() {
                        $(this).draggable({
                            helper: "clone"
                        })
                    });

                    //拖动添加矩形
                    $(DOMObj).droppable({
                        accept: ".state",
                        drop: function(event, currNode) {
                            $(paper).trigger("addrect", [currNode.helper.attr("type"), {
                                attr: {
                                    x: currNode.helper.offset().left,
                                    y: currNode.helper.offset().top
                                }
}])
                            }
                        });

                        $("#myflow_new").click(function() {
                            rectArray = {};
                            pathArray = {};
                            myflow.config.props.props = {};
                        });

                        //保存事件,封装图形JSON数据
                        $("#myflow_save").click(function() {
                            var jsonStr = "{states:{";
                            for (var rect in rectArray) {
                                if (rectArray[rect]) {
                                    jsonStr += rectArray[rect].getId() + ":" + rectArray[rect].toJson() + ","
                                }
                            }
                            if (jsonStr.substring(jsonStr.length - 1, jsonStr.length) == ",") {
                                jsonStr = jsonStr.substring(0, jsonStr.length - 1)
                            }
                            jsonStr += "},paths:{";
                            for (var path in pathArray) {
                                if (pathArray[path]) {
                                    jsonStr += pathArray[path].getId() + ":" + pathArray[path].toJson() + ","
                                }
                            }
                            if (jsonStr.substring(jsonStr.length - 1, jsonStr.length) == ",") {
                                jsonStr = jsonStr.substring(0, jsonStr.length - 1)
                            }
                            jsonStr += "},props:{props:{";
                            for (var path in myflow.config.props.props) {
                                jsonStr += path + ":{value:'" + myflow.config.props.props[path].value + "'},";
                            }
                            if (jsonStr.substring(jsonStr.length - 1, jsonStr.length) == ",") {
                                jsonStr = jsonStr.substring(0, jsonStr.length - 1)
                            }
                            jsonStr += "}}}";
                            myflow.config.tools.save.onclick(jsonStr);
                        });
                        new myflow.props({}, paper)
                    }

                    //流程数据的加载
                    if (myflowObj.restore) {
                        var restore = myflowObj.restore;
                        //已存在矩形数据集合，临时变量，便于路径连线处理
                        var rectArrayTemp = {};
                        //矩形数据加载
                        if (restore.states) {
                            for (var rectId in restore.states) {
                                var rect = new myflow.rect($.extend(true, {},
								myflow.config.tools.states[restore.states[rectId].type],
								restore.states[rectId]), paper);
                                //添加矩形数据到流程图显示
                                rect.restore(restore.states[rectId]);
                                //矩形数据临时集合
                                rectArrayTemp[rectId] = rect;
                                //本流程图中矩形数据集合
                                rectArray[rect.getId()] = rect;
                            }
                        }
                        //路径数据加载
                        if (restore.paths) {
                            for (var pathId in restore.paths) {
                                var path = new myflow.path($.extend(true, {}, myflow.config.tools.path,
								restore.paths[pathId]), paper, rectArrayTemp[restore.paths[pathId].from],
								rectArrayTemp[restore.paths[pathId].to]);
                                //添加路径数据到流程图显示
                                path.restore(restore.paths[pathId]);
                                //本流程图中路径数据集合
                                pathArray[path.getId()] = path;
                            }
                        }



                    }

                    //历史流程节点
                    var hiRects = myflow.config.historyRects;
                    //当前活动节点
                    var acRects = myflow.config.activeRects;
                    if (hiRects.rects.length || acRects.rects.length) {
                        //流程数据
                        var flowData = {};
                        for (var pathId in pathArray) {
                            if (!flowData[pathArray[pathId].from().text()]) {
                                flowData[pathArray[pathId].from().text()] = {
                                    rect: pathArray[pathId].from(),
                                    paths: {}
                                }
                            }
                            flowData[pathArray[pathId].from().text()].paths[pathArray[pathId].text()] = pathArray[pathId];
                            if (!flowData[pathArray[pathId].to().text()]) {
                                flowData[pathArray[pathId].to().text()] = {
                                    rect: pathArray[pathId].to(),
                                    paths: {}
                                }
                            }
                        }
                        //历史流程数据解析封装
                        for (var i = 0; i < hiRects.rects.length; i++) {
                            if (flowData[hiRects.rects[i].name]) {
                                flowData[hiRects.rects[i].name].rect.attr(hiRects.rectAttr)
                            }
                            for (var j = 0; j < hiRects.rects[i].paths.length; j++) {
                                if (flowData[hiRects.rects[i].name].paths[hiRects.rects[i].paths[j]]) {
                                    flowData[hiRects.rects[i].name].paths[hiRects.rects[i].paths[j]]
								.attr(hiRects.pathAttr)
                                }
                            }
                        }

                        //当前活动流程数据解析封装
                        for (var i = 0; i < acRects.rects.length; i++) {
                            if (flowData[acRects.rects[i].name]) {
                                flowData[acRects.rects[i].name].rect.attr(acRects.rectAttr)
                            }
                            for (var j = 0; j < acRects.rects[i].paths.length; j++) {
                                if (flowData[acRects.rects[i].name].paths[acRects.rects[i].paths[j]]) {
                                    flowData[acRects.rects[i].name].paths[acRects.rects[i].paths[j]]
								.attr(acRects.pathAttr)
                                }
                            }
                        }
                    }
                };

                //obj：myflow对象
                //页面元素：如<div id='myflow'></div>
                $.fn.myflow = function(obj) {
                    return this.each(function() {
                        myflow.init(this, obj)
                    })
                };
                $.myflow = myflow;
            })(jQuery);